using DoAnWebGamingGear.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Mail;
using DoAnWebGamingGear.Libraries;
using System.Configuration;
using System.Web;
using System.Data.Entity;

namespace DoAnWebGamingGear.Controllers
{
    public class PaymentController : Controller
    {
        private GamingGearDBContext db = new GamingGearDBContext();

        [HttpGet]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var cartItems = Session[userId + "_Cart"] as List<CartItem>;
            ViewBag.CartItems = cartItems;
            ViewBag.TotalAmount = cartItems?.Sum(item => item.Products.Price * item.shopping_quantity) ?? 0;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(string customerName, string customerPhone, string customerAddress, string customerEmail, string paymentMethod, int? TypePaymentVN)
        {
            var userId = User.Identity.GetUserId();
            var cartItems = Session[userId + "_Cart"] as List<CartItem>;
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart"); // Nếu giỏ hàng trống, quay lại trang giỏ hàng
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Order order = new Order
                    {
                        OrderId = Guid.NewGuid().ToString("N").Substring(0, 8),
                        UserId = User.Identity.GetUserId(),
                        CreatedDate = DateTime.Now,
                        Status = "Chưa thanh toán",
                        CustomerName = customerName,
                        CustomerPhone = customerPhone,
                        CustomerAddress = customerAddress,
                        CustomerEmail = customerEmail,
                        PaymentMethod = paymentMethod,
                        TotalAmount = 0
                    };

                    db.Orders.Add(order);
                    db.SaveChanges(); // Lưu Order trước để có OrderId

                    decimal totalAmount = 0;

                    foreach (var item in cartItems)
                    {
                        var product = db.Products.FirstOrDefault(p => p.ProductID == item.ProductID);
                        if (product != null)
                        {
                            OrderDetail orderDetail = new OrderDetail
                            {
                                OrderId = order.OrderId,
                                ProductID = item.ProductID,
                                Quantity = item.shopping_quantity,
                                Price = product.Price * item.shopping_quantity,
                                UnitPrice = product.Price
                            };

                            db.OrderDetails.Add(orderDetail); // Thêm OrderDetail vào cơ sở dữ liệu
                            totalAmount += item.shopping_quantity * product.Price;
                        }
                    }

                    order.TotalAmount = totalAmount;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges(); // Cập nhật lại TotalAmount của Order

                    // Lưu trữ đơn hàng tạm thời trong session
                    Session["PendingOrder"] = order;

                    if (paymentMethod == "Chuyển khoản ngân hàng" && TypePaymentVN.HasValue)
                    {
                        string paymentUrl = UrlPayment(TypePaymentVN.Value, order.OrderId);
                        transaction.Commit();
                        return Redirect(paymentUrl);
                    }

                    // Xóa giỏ hàng sau khi thanh toán
                    Session[userId + "_Cart"] = null;
                    transaction.Commit();

                    return RedirectToAction("OrderSuccess", "Payment", new { orderId = order.OrderId });
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return RedirectToAction("Index", "ShoppingCart"); // Nếu có lỗi, quay lại trang giỏ hàng
                }
            }
        }

        public ActionResult VnPayReturn()
        {
            string orderId = null;
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                orderId = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var itemOrder = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
                        if (itemOrder != null)
                        {
                            itemOrder.Status = "Đã thanh toán";
                            db.Orders.Attach(itemOrder);
                            db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    //displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.ThanhToanThanhCong = "Số tiền thanh toán: " + vnp_Amount.ToString("N0").Replace(",", ".") + "đ";
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                }
            }
            var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        // Trang hiển thị sau khi thanh toán thành công
        public ActionResult OrderSuccess(string orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        public string UrlPayment(int TypePaymentVN, string orderCode)
        {
            var order = Session["PendingOrder"] as Order;

            // Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"];
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"];
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];

            // Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (decimal)order.TotalAmount * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString());
            switch (TypePaymentVN)
            {
                case 1:
                    vnpay.AddRequestData("vnp_BankCode", "DEFAULT");
                    break;
                case 2:
                    vnpay.AddRequestData("vnp_BankCode", "VNBANK");
                    break;
                case 3:
                    vnpay.AddRequestData("vnp_BankCode", "INTCARD");
                    break;
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId);

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }

        public ActionResult PaymentReturn()
        {
            var vnpay = new VnPayLibrary();
            var response = Request.QueryString;
            foreach (string s in response)
            {
                if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(s, response[s]);
                }
            }

            string hashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
            bool checkSignature = vnpay.ValidateSignature(response["vnp_SecureHash"], hashSecret);

            if (checkSignature)
            {
                string orderId = vnpay.GetResponseData("vnp_TxnRef");
                string responseCode = vnpay.GetResponseData("vnp_ResponseCode");
                if (responseCode == "00")
                {
                    // Thanh toán thành công
                    var order = Session["PendingOrder"] as Order;
                    if (order != null && order.OrderId == orderId)
                    {
                        db.Orders.Add(order);
                        db.SaveChanges();

                        // Xóa giỏ hàng sau khi thanh toán
                        var userId = User.Identity.GetUserId();
                        Session[userId + "_Cart"] = null;
                        Session["PendingOrder"] = null;

                        ViewBag.Message = "Thanh toán thành công!";
                    }
                }
                else
                {
                    // Thanh toán thất bại
                    ViewBag.Message = "Thanh toán thất bại!";
                }
            }
            else
            {
                ViewBag.Message = "Chữ ký không hợp lệ!";
            }

            return View();
        }
        // Gửi email xác nhận đơn hàng
        /*private void SendOrderConfirmationEmail(Order order)
        {
            try
            {
                string customerEmail = order.CustomerEmail;
                string subject = "Order Confirmation";
                string body = $"Dear {order.CustomerName},\n\nThank you for your order. Your order ID is {order.OrderId}.\n\nTotal Amount: {order.TotalAmount:C}.\n\nBest regards,\nGaming Gear Team";

                // Thiết lập thông tin email
                MailMessage mail = new MailMessage("your-email@example.com", customerEmail);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtpClient = new SmtpClient("smtp.example.com"); // Thay thế bằng thông tin SMTP server của bạn
                smtpClient.Port = 587; // Port SMTP (tùy thuộc vào nhà cung cấp)
                smtpClient.Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-password"); // Thông tin đăng nhập SMTP
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu email không gửi được
                System.Diagnostics.Debug.WriteLine("Error sending email: " + ex.Message);
            }
        }*/
    }
}
