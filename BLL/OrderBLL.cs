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

        public decimal TinhDoanhThuTheoKhoangThoiGian(DateTime batDau, DateTime ketThuc)
        {
            return odal.TinhDoanhThuTheoKhoangThoiGian(batDau, ketThuc);
        }

        public int TongSoHoaDonTheoKhoangThoiGian(DateTime batDau, DateTime ketThuc)
        {
            return odal.TongSoHoaDonTheoKhoangThoiGian(batDau, ketThuc);
        }

        public Dictionary<DateTime?, decimal> ThongKeTongDoanhThuCuaTungNgay(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            return odal.ThongKeTongDoanhThuCuaTungNgay(ngayBatDau, ngayKetThuc);
        }

        public Dictionary<int, decimal> ThongKeTongDoanhThuTheoGioTrongNgay(DateTime ngay)
        {
            return odal.ThongKeTongDoanhThuTheoGioTrongNgay(ngay);
        }

        public List<ThongKeDoanhThuTheoThang> ThongKeTongDoanhThuTheoThangTrongNam(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            return odal.ThongKeTongDoanhThuTheoThangTrongNam(ngayBatDau, ngayKetThuc);
        }
    }
}
