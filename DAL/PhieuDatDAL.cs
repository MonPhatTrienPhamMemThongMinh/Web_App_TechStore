using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class PhieuDatDAL
    {
        private DBGAMINGGEARDataContext db;
        public PhieuDatDAL()
        {
            this.db = new DBGAMINGGEARDataContext();
        }
        public List<PhieuDat> LayDanhSachPhieuDat()
        {
            List<PhieuDat> phieuDats = db.PhieuDats.OrderByDescending(pd => pd.MaPhieuDat).Select(pd => pd).ToList<PhieuDat>();
            foreach(PhieuDat phieuDat in phieuDats)
            {
                phieuDat.tenNhanVien = db.AspNetUsers.Where(nv => nv.Id == phieuDat.UserID).Select(nv => nv.FullName).FirstOrDefault();
                phieuDat.tenNhaCungCap = db.NhaCungCaps.Where(ncc => ncc.maNhaCungCap == phieuDat.MaNhaCungCap).Select(ncc => ncc.tenNhaCungCap).FirstOrDefault();
            }
            return phieuDats;
        }
        public List<PhieuDat> LayDanhSachPhieuDatDuocDuyet()
        {
            //List<PhieuDat> phieuDats = db.PhieuDats.Where(pd=>pd.TrangThai == "Đã duyệt" && pd.TrangThaiXacNhan == "Đã chấp thuận").OrderByDescending(pd=>pd.MaPhieuDat).Select(pd => pd).ToList<PhieuDat>();
            //foreach (PhieuDat phieuDat in phieuDats)
            //{
            //    phieuDat.tenNhanVien = db.AspNetUsers.Where(nv => nv.Id == phieuDat.UserID).Select(nv => nv.FullName).FirstOrDefault();
            //    phieuDat.tenNhaCungCap = db.NhaCungCaps.Where(ncc => ncc.maNhaCungCap == phieuDat.MaNhaCungCap).Select(ncc => ncc.tenNhaCungCap).FirstOrDefault();
            //}
            //return phieuDats;
            return null;
        }
        public List<PhieuDat> LocDanhSachPhieuDatTheoNgayLap(DateTime ngayLap)
        {
            List<PhieuDat> phieuDats = db.PhieuDats.Where(pd=>pd.NgayLap == ngayLap).Select(pd => pd).ToList<PhieuDat>();
            foreach (PhieuDat phieuDat in phieuDats)
            {
                phieuDat.tenNhanVien = db.AspNetUsers.Where(nv => nv.Id == phieuDat.UserID).Select(nv => nv.FullName).FirstOrDefault();
                phieuDat.tenNhaCungCap = db.NhaCungCaps.Where(ncc => ncc.maNhaCungCap == phieuDat.MaNhaCungCap).Select(ncc => ncc.tenNhaCungCap).FirstOrDefault();
            }
            return phieuDats;
        }
        public PhieuDat TimKiemPhieuDatTheoMaPhieuDat(string maPhieuDat)
        {
            try
            {
                PhieuDat phieuDats = db.PhieuDats.Where(pd => pd.MaPhieuDat == maPhieuDat).Select(pd => pd).First();
                phieuDats.tenNhanVien = db.AspNetUsers.Where(nv => nv.Id == phieuDats.UserID).Select(nv => nv.FullName).FirstOrDefault();
                phieuDats.tenNhaCungCap = db.NhaCungCaps.Where(ncc => ncc.maNhaCungCap == phieuDats.MaNhaCungCap).Select(ncc => ncc.tenNhaCungCap).FirstOrDefault();
                return phieuDats;
            }
            catch (Exception)
            {
                return null;
            }           
        }
        public string TaoMaPhieuDat()
        {
            string maPhieuDat = db.PhieuDats.OrderByDescending(pd => pd.MaPhieuDat).Select(pd => pd.MaPhieuDat).FirstOrDefault();
            if (maPhieuDat!=null)
            {
                return maPhieuDat;
            }
            return "PD000000000";
        }
        public bool TaoPhieuDat(PhieuDat pPhieuDat)
        {
            try
            {
                db.PhieuDats.InsertOnSubmit(pPhieuDat);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaPhieuDat(PhieuDat pPhieuDat)
        {
            try
            {
                PhieuDat phieuDatEdited = db.PhieuDats.Where(pd => pd.MaPhieuDat == pPhieuDat.MaPhieuDat).Select(pd => pd).FirstOrDefault();
                phieuDatEdited.MaNhaCungCap = pPhieuDat.MaNhaCungCap;
                phieuDatEdited.UserID = pPhieuDat.UserID;
                phieuDatEdited.NgayCapNhat = pPhieuDat.NgayCapNhat;
                phieuDatEdited.SoLuong = pPhieuDat.SoLuong;
                phieuDatEdited.TongTien = pPhieuDat.TongTien;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DuyetPhieuDat(string maPhieuDat,string trangThai)
        {
            try
            {
                PhieuDat phieuDatEdited = db.PhieuDats.Where(pd => pd.MaPhieuDat == maPhieuDat).Select(pd => pd).FirstOrDefault();
                phieuDatEdited.TrangThai = trangThai;
                phieuDatEdited.NgayCapNhat = DateTime.Now;
                if (trangThai == "Đã duyệt")
                {
                    //phieuDatEdited.TrangThaiXacNhan = null;
                    //phieuDatEdited.GhiChu = null;
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XacNhanPhieuDat(string maPhieuDat,string trangThai)
        {
            try
            {
                PhieuDat phieuDatEdited = db.PhieuDats.Where(pd => pd.MaPhieuDat == maPhieuDat).Select(pd => pd).FirstOrDefault();
                //phieuDatEdited.TrangThaiXacNhan = trangThai;
                phieuDatEdited.NgayCapNhat = DateTime.Now;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaPhieuDat(PhieuDat pPhieuDat)
        {
            try
            {
                db.XoaPhieuDat_Proc(pPhieuDat.MaPhieuDat);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaPhieuDat(string maPhieuDat)
        {
            try
            {
                db.XoaPhieuDat_Proc(maPhieuDat);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
