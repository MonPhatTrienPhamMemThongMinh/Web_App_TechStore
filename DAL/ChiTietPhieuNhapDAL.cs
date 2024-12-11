using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class ChiTietPhieuNhapDAL
    {
        private DBGAMINGGEARDataContext db;
        public ChiTietPhieuNhapDAL() 
        { 
            this.db = new DBGAMINGGEARDataContext();
        }
        public List<ChiTietPhieuNhap> LayDanhSachChiTietPhieuNhap(string maPhieuNhap)
        {
            List<ChiTietPhieuNhap> chiTietPhieuNhaps = db.ChiTietPhieuNhaps.Where(ctpm=>ctpm.MaPhieuNhap == maPhieuNhap).Select(ctpn => ctpn).ToList<ChiTietPhieuNhap>();
            foreach (ChiTietPhieuNhap chiTietPhieuNhap in chiTietPhieuNhaps)
            {
                chiTietPhieuNhap.tenSanPham = db.Products.Where(sp => sp.ProductID == chiTietPhieuNhap.ProductID).Select(sp => sp.ProductName).FirstOrDefault();
            }
            return chiTietPhieuNhaps;
        }
        public bool TaoChiTietPhieuNhap (ChiTietPhieuNhap pChiTietPhieuNhap)
        {
            try
            {
                db.ChiTietPhieuNhaps.InsertOnSubmit(pChiTietPhieuNhap);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SuaChiTietPhieuNhap(ChiTietPhieuNhap pChiTietPhieuNhap)
        {
            try
            {
                ChiTietPhieuNhap chiTietPhieuNhapEdited = db.ChiTietPhieuNhaps.Where(ctpn => ctpn.MaPhieuDat == pChiTietPhieuNhap.MaPhieuNhap && ctpn.MaPhieuNhap == pChiTietPhieuNhap.MaPhieuNhap && ctpn.ProductID == pChiTietPhieuNhap.ProductID).Select(ctpn => ctpn).FirstOrDefault();
                chiTietPhieuNhapEdited.SoLuong = pChiTietPhieuNhap.SoLuong;
                chiTietPhieuNhapEdited.DonGia = pChiTietPhieuNhap.DonGia;
                chiTietPhieuNhapEdited.TongTien = pChiTietPhieuNhap.TongTien;
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }        
    }
}
