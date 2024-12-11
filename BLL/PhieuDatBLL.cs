using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class PhieuDatBLL
    {
        private PhieuDatDAL phieuDatDAL;
        public PhieuDatBLL()
        {
            this.phieuDatDAL = new PhieuDatDAL();
        }
        public List<PhieuDat> LayDanhSachPhieuDat()
        {
            return phieuDatDAL.LayDanhSachPhieuDat();
        }
        public List<PhieuDat> LayDanhSachPhieuDatDaDuyet()
        {
            return phieuDatDAL.LayDanhSachPhieuDatDuocDuyet();
        }
        public List<PhieuDat> LocDanhSachPhieuDatTheoNgayLap(DateTime ngayLap)
        {
            return phieuDatDAL.LocDanhSachPhieuDatTheoNgayLap(ngayLap);
        }
        public PhieuDat TimKiemPhieuDatTheoMaPhieuDat(string maPhieuDat)
        {
            return phieuDatDAL.TimKiemPhieuDatTheoMaPhieuDat(maPhieuDat);
        }
        public string TaoMaPhieuDat()
        {
            string maPhieuDat = phieuDatDAL.TaoMaPhieuDat();
            string prefixPart = maPhieuDat.Substring(0, 2);
            string numberPart = maPhieuDat.Substring(2);
            int number = int.Parse(numberPart) + 1;
            string newNumberPart = number.ToString("D" + numberPart.Length);
            string newMaPhieuDat = prefixPart + newNumberPart;
            return newMaPhieuDat;
        }
        public bool TaoPhieuDat(PhieuDat pPhieuDat)
        {
            return phieuDatDAL.TaoPhieuDat(pPhieuDat);
        }
        public bool SuaPhieuDat(PhieuDat pPhieuDat)
        {
            return phieuDatDAL.SuaPhieuDat(pPhieuDat);
        }
        public bool DuyetPhieuDat(string maPhieuDat,string trangThai)
        {
            return phieuDatDAL.DuyetPhieuDat(maPhieuDat,trangThai);
        }
        public bool XacNhanPhieuDat(string maPhieuDat,string trangThai)
        {
            return phieuDatDAL.XacNhanPhieuDat(maPhieuDat, trangThai);
        }
        public bool XoaPhieuDat(PhieuDat pPhieuDat)
        {
            return phieuDatDAL.XoaPhieuDat(pPhieuDat);
        }
        public bool XoaPhieuDat(string maPhieuDat)
        {
            return phieuDatDAL.XoaPhieuDat(maPhieuDat);
        }
    }
}
