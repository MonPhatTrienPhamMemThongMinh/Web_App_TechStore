using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class ChiTietPhieuDatDAL
    {
        private DBGAMINGGEARDataContext db;
        public ChiTietPhieuDatDAL()
        {
            this.db = new DBGAMINGGEARDataContext();
        }
        public List<ChiTietPhieuDat> LayChiTietPhieuDat(string maPhieuDat)
        {
            List<ChiTietPhieuDat> chiTietPhieuDats = db.ChiTietPhieuDats.Where(ctpd => ctpd.MaPhieuDat == maPhieuDat).Select(ctpd => ctpd).ToList<ChiTietPhieuDat>();
            foreach(ChiTietPhieuDat chiTietPhieuDat in chiTietPhieuDats)
            {
                chiTietPhieuDat.tenSanPham = db.Products.Where(sp => sp.ProductID == chiTietPhieuDat.ProductID).Select(sp => sp.ProductName).First();
            }
            return chiTietPhieuDats;
        }
        public bool KiemTraTonTaiChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            bool result = (db.ChiTietPhieuDats.Where(ctpd => ctpd.MaPhieuDat == pChiTietPhieuDat.MaPhieuDat && ctpd.ProductID == pChiTietPhieuDat.ProductID).Select(ctpd => ctpd).FirstOrDefault() != null) ? true : false;
            return result;
        }
        public bool TaoChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            try
            {
                db.ChiTietPhieuDats.InsertOnSubmit(pChiTietPhieuDat);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SuaChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            try
            {
                ChiTietPhieuDat chiTietPhieuDatEdited = db.ChiTietPhieuDats.Where(ctpd => ctpd.MaPhieuDat == pChiTietPhieuDat.MaPhieuDat && ctpd.ProductID == pChiTietPhieuDat.ProductID).Select(ctpd => ctpd).FirstOrDefault();                
                chiTietPhieuDatEdited.SoLuongDat = pChiTietPhieuDat.SoLuongDat;
                chiTietPhieuDatEdited.DonGia = pChiTietPhieuDat.DonGia;
                chiTietPhieuDatEdited.TongTien = pChiTietPhieuDat.TongTien;
                db.SubmitChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool XoaChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            try
            {
                ChiTietPhieuDat chiTietPhieuDatDeleted = db.ChiTietPhieuDats.Where(ctpd => ctpd.MaPhieuDat == pChiTietPhieuDat.MaPhieuDat && ctpd.ProductID == pChiTietPhieuDat.ProductID).Select(ctpd => ctpd).FirstOrDefault();
                db.ChiTietPhieuDats.DeleteOnSubmit(chiTietPhieuDatDeleted);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaChiTietPhieuDat(string maPhieuDat,string maSanPham)
        {
            try
            {
                ChiTietPhieuDat chiTietPhieuDatDeleted = db.ChiTietPhieuDats.Where(ctpd => ctpd.MaPhieuDat == maPhieuDat && ctpd.ProductID == maSanPham).Select(ctpd => ctpd).FirstOrDefault();
                db.ChiTietPhieuDats.DeleteOnSubmit(chiTietPhieuDatDeleted);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
