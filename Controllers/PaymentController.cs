using DoAnWebGamingGear.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Mail;

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
        public ActionResult Checkout(string customerName, string customerPhone, string customerAddress, string customerEmail, string paymentMethod)
        {
            var userId = User.Identity.GetUserId();
            // Lấy thông tin giỏ hàng từ session
            var cartItems = Session[userId + "_Cart"] as List<CartItem>;
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart"); // Nếu giỏ hàng trống, quay lại trang giỏ hàng
            }

            Order order = new Order
            {
                OrderId = Guid.NewGuid().ToString("N").Substring(0, 8),
                UserId = User.Identity.GetUserId(),
                CreatedDate = DateTime.Now,
                Status = false,
                CustomerName = customerName,
                CustomerPhone = customerPhone,
                CustomerAddress = customerAddress,
                CustomerEmail = customerEmail,
                PaymentMethod = paymentMethod,
                TotalAmount = 0
            };

            db.Orders.Add(order);
            db.SaveChanges();

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

                    db.OrderDetails.Add(orderDetail);

                    totalAmount += item.shopping_quantity * product.Price;
                }
            }

            order.TotalAmount = totalAmount;
            db.SaveChanges();

            // Xóa giỏ hàng sau khi thanh toán
            Session[userId + "_Cart"] = null;

            // SendOrderConfirmationEmail(order);

            return RedirectToAction("OrderSuccess", "Payment", new { orderId = order.OrderId });
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
    }
}
