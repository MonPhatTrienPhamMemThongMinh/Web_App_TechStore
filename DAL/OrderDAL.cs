using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public OrderDAL() { }

        public List<Order> LoadAllOrders()
        {
            try
            {
                var orders = db.Orders.Select(hd => hd).OrderByDescending(hd => hd.OrderId).ToList<Order>();
                //foreach (var order in orders)
                //{
                //    if (order.Status == true)
                //    {
                //        order.statusText = "Đã thanh toán";
                //    }
                //    else
                //    {
                //        order.statusText = "Chưa thanh toán";
                //    }
                //}
                return orders;
            }
            catch
            {
                return new List<Order>();
            }

        }
        public static string RemoveVietnameseDaus(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string[] daus = new string[]
            {
                "aáàảãạăắằẳẵặâấầẩẫậ", "AÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ",
                "dđ", "DĐ",
                "eéèẻẽẹêếềểễệ", "EÉÈẺẼẸÊẾỀỂỄỆ",
                "iíìỉĩị", "IÍÌỈĨỊ",
                "oóòỏõọôốồổỗộơớờởỡợ", "OÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ",
                "uúùủũụưứừửữự", "UÚÙỦŨỤƯỨỪỬỮỰ",
                "yýỳỷỹỵ", "YÝỲỶỸỴ"
            };

            foreach (var dau in daus)
            {
                foreach (var ch in dau.Skip(1))
                {
                    input = input.Replace(ch, dau[0]);
                }
            }

            return input;
        }

        public List<Order> TimKiemVaLocHoaDon(string tieuChi, string giaTriTimKiem, DateTime ngayBatDau, DateTime ngayKetThuc, string trangThai)
        {
            try
            {
                var orders = db.Orders.Where(hd => hd.CreatedDate.Date >= ngayBatDau.Date
                                            && hd.CreatedDate.Date <= ngayKetThuc.Date)
                                        .ToList();
                //foreach (var order in orders)
                //{
                //    order.statusText = order.Status == true ? "Đã thanh toán" : "Chưa thanh toán";
                //}

                if (!string.IsNullOrEmpty(giaTriTimKiem))
                {
                    giaTriTimKiem = RemoveVietnameseDaus(giaTriTimKiem.ToLower());
                    switch (tieuChi)
                    {
                        case "Các tiêu chí":
                            break;
                        case "Mã hóa đơn":
                            orders = orders.Where(hd => RemoveVietnameseDaus(hd.OrderId.ToLower()).Contains(giaTriTimKiem)).ToList();
                            break;
                        case "Tên khách hàng":
                            orders = orders
                                .Where(hd => hd.CustomerName != null && RemoveVietnameseDaus(hd.CustomerName.ToLower())
                                .Contains(giaTriTimKiem)).ToList();
                            break;
                    }
                }
                switch (trangThai)
                {
                    case "Chọn trạng thái..":
                        break;
                    case "Chưa thanh toán":
                        orders = orders.Where(hd => hd.statusText == trangThai).ToList();
                        break;
                    case "Đã thanh toán":
                        orders = orders.Where(hd => hd.statusText == trangThai).ToList();
                        break;
                }
                return orders;
            }
            catch
            {
                return new List<Order>();
            }
        }

        public Order LoadHoaDonTheoMa(string mahd)
        {
            Order order = db.Orders.FirstOrDefault(hd => hd.OrderId == mahd);
            //if (order.Status == true)
            //{
            //    order.statusText = "Đã thanh toán";
            //}
            //else
            //{
            //    order.statusText = "Chưa thanh toán";
            //}
            return order;
        }

        // Tất cả hóa đơn
        public decimal TinhTongDoanhThu()
        {
            if (db.Orders.Any())
            {
                return db.Orders.Sum(hd => hd.TotalAmount);
            }
            return 0;
        }

        public int TongSoHoaDon()
        {
            if (db.Orders.Any())
            {
                return db.Orders.Count();
            }
            return 0;
        }
    }
}
