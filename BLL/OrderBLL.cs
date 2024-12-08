using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class OrderBLL
    {
        OrderDAL odal = new OrderDAL();
        public OrderBLL() { }

        public List<Order> LoadAllOrders()
        {
            return odal.LoadAllOrders();
        }

        public List<Order> TimKiemVaLocHoaDon(string tieuChi, string giaTriTimKiem, DateTime ngayBatDau, DateTime ngayKetThuc, string trangThai)
        {
            return odal.TimKiemVaLocHoaDon(tieuChi, giaTriTimKiem, ngayBatDau, ngayKetThuc, trangThai);
        }

        public Order LoadHoaDonTheoMa(string mahd)
        {
            return odal.LoadHoaDonTheoMa(mahd);
        }

        public decimal TinhTongDoanhThu()
        {
            return odal.TinhTongDoanhThu();
        }

        public int TongSoHoaDon()
        {
            return odal.TongSoHoaDon();
        }
    }
}
