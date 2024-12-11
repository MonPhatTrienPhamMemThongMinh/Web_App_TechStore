using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class PhieuNhapBLL
    {
        private PhieuNhapDAL phieuNhapDAL;
        public PhieuNhapBLL()
        {
            this.phieuNhapDAL = new PhieuNhapDAL();
        }
        public List<PhieuNhap> LayDanhSachPhieuNhap()
        {
            return phieuNhapDAL.LayDanhSachPhieuNhap();
        }
        public PhieuNhap TimKiemPhieuNhapTheoMaPhieuNhap(string maPhieuNhap)
        {
            return phieuNhapDAL.TimKiemPhieuNhapTheoMaPhieuNhap(maPhieuNhap);
        }
        public List<PhieuNhap> TimKiemPhieuNhapTheoMaPhieuDat(string maPhieuDat)
        {
            return phieuNhapDAL.TimKiemPhieuNhapTheoMaPhieuDat(maPhieuDat);
        }
        public List<PhieuNhap> LocDanhSachPhieuNhapTheoNgayLap(DateTime ngayNhap)
        {
            return phieuNhapDAL.LocDanhSachPhieuNhapTheoNgayLap(ngayNhap);
        }
        public string TaoMaPhieuNhap()
        {
            string maPhieuNhap = phieuNhapDAL.TaoMaPhieuNhap();
            string prefixPart = maPhieuNhap.Substring(0, 2);
            string numberPart = maPhieuNhap.Substring(2);
            int number = int.Parse(numberPart) + 1;
            string newNumberPart = number.ToString("D" + numberPart.Length);
            string newMaPhieuNhap = prefixPart + newNumberPart;
            return newMaPhieuNhap;
        }
        public bool TaoPhieuNhap(PhieuNhap pPhieuNhap)
        {
            return phieuNhapDAL.TaoPhieuNhap(pPhieuNhap);
        }
        public bool XoaPhieuNhap(string maPhieuNhap,int? soLan)
        {
            return phieuNhapDAL.XoaPhieuNhap(maPhieuNhap,soLan);
        }

        public decimal TinhTongTienPhieuNhapTheoKhoangThoiGian(DateTime batDau, DateTime ketThuc)
        {
            return phieuNhapDAL.TinhTongTienPhieuNhapTheoKhoangThoiGian(batDau, ketThuc);
        }
    }
}
