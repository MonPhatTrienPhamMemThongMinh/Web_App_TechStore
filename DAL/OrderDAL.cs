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
        public decimal TinhDoanhThuTheoKhoangThoiGian(DateTime batDau, DateTime ketThuc)
        {
            return db.Orders
                .Where(hd => hd.CreatedDate.Date >= batDau.Date && hd.CreatedDate.Date <= ketThuc.Date)
                .Sum(hd => (decimal?)hd.TotalAmount) ?? 0;
            return 0;
        }

        public int TongSoHoaDonTheoKhoangThoiGian(DateTime batDau, DateTime ketThuc)
        {
            return db.Orders
                .Where(hd => hd.CreatedDate.Date >= batDau.Date && hd.CreatedDate.Date <= ketThuc.Date)
                .Count();
        }

        public Dictionary<DateTime?, decimal> ThongKeTongDoanhThuCuaTungNgay(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var doanhThuTheoNgay = db.Orders
                                    .Where(hd => hd.CreatedDate.Date >= ngayBatDau.Date && hd.CreatedDate.Date <= ngayKetThuc.Date)
                                    .GroupBy(hd => hd.CreatedDate.Date)
                                    .Select(tk => new
                                    {
                                        Ngay = tk.Key,
                                        TongDoanhThu = tk.Sum(hd => (decimal?)hd.TotalAmount) ?? 0
                                    }).ToDictionary(x => (DateTime?)x.Ngay, x => x.TongDoanhThu);

            if (!doanhThuTheoNgay.Any())
            {
                return new Dictionary<DateTime?, decimal>();
            }
            return doanhThuTheoNgay;
        }

        // Lọc hôm nay
        public Dictionary<int, decimal> ThongKeTongDoanhThuTheoGioTrongNgay(DateTime ngay)
        {
            DateTime ngayBatDau = ngay.Date;
            DateTime ngayKetThuc = ngayBatDau.AddDays(1);
            var doanhThuTheoGio = db.Orders
                                    .Where(hd => hd.CreatedDate >= ngayBatDau && hd.CreatedDate < ngayKetThuc)
                                    .GroupBy(hd => hd.CreatedDate.Hour)
                                    .Select(tk => new
                                    {
                                        Gio = tk.Key,
                                        TongDoanhThu = tk.Sum(hd => (decimal?)hd.TotalAmount) ?? 0
                                    }).ToDictionary(x => x.Gio, x => x.TongDoanhThu);

            if (!doanhThuTheoGio.Any())
            {
                return new Dictionary<int, decimal>();
            }
            return doanhThuTheoGio;
        }

        // Lọc năm nay
        public List<ThongKeDoanhThuTheoThang> ThongKeTongDoanhThuTheoThangTrongNam(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var doanhThuTheoThang = db.Orders
                                  .Where(hd => hd.CreatedDate >= ngayBatDau && hd.CreatedDate <= ngayKetThuc)
                                  .GroupBy(hd => new { hd.CreatedDate.Year, hd.CreatedDate.Month })
                                  .Select(tk => new ThongKeDoanhThuTheoThang
                                  {
                                      Thang = new DateTime(tk.Key.Year, tk.Key.Month, 1),
                                      TongDoanhThu = TinhDoanhThuTheoKhoangThoiGian(ngayBatDau, ngayKetThuc)
                                  })
                                  .ToList();

            if (!doanhThuTheoThang.Any())
            {
                return new List<ThongKeDoanhThuTheoThang>();
            }
            return doanhThuTheoThang;
        }
    }
}
