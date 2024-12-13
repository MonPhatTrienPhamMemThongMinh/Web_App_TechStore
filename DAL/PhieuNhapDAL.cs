using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class PhieuNhapDAL
    {
        private DBGAMINGGEARDataContext db;
        public PhieuNhapDAL()
        {
            this.db = new DBGAMINGGEARDataContext();
        }
        public List<PhieuNhap> LayDanhSachPhieuNhap()
        {
            List<PhieuNhap> phieuNhaps = db.PhieuNhaps.Select(pn => pn).ToList<PhieuNhap>();            
            return phieuNhaps;
        }
        public PhieuNhap TimKiemPhieuNhapTheoMaPhieuNhap(string maPhieuNhap)
        {
            try
            {
                PhieuNhap phieuNhaps = db.PhieuNhaps.Where(pn => pn.MaPhieuNhap  == maPhieuNhap).Select(pn => pn).FirstOrDefault();                
                return phieuNhaps;
            }
            catch (Exception)
            {
                return null;
            }            
        }
        public List<PhieuNhap> TimKiemPhieuNhapTheoMaPhieuDat(string maPhieuDat)
        {
            try
            {
                List<PhieuNhap> phieuNhaps = db.PhieuNhaps.Where(pn => pn.MaPhieuDat == maPhieuDat).Select(pn => pn).ToList<PhieuNhap>();                               
                return phieuNhaps;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string TaoMaPhieuNhap()
        {
            string maPhieuNhap = db.PhieuNhaps.OrderByDescending(pn => pn.MaPhieuNhap).Select(pn => pn.MaPhieuNhap).FirstOrDefault();
            if (maPhieuNhap != null)
            {
                return maPhieuNhap;
            }
            return "PN000000000";
        }
        public List<PhieuNhap> LocDanhSachPhieuNhapTheoNgayLap(DateTime ngayLap)
        {
            List<PhieuNhap> phieuNhaps = db.PhieuNhaps.Where(pn => pn.NgayNhap.Date == ngayLap.Date).Select(pn => pn).ToList<PhieuNhap>();            
            return phieuNhaps;
        }
        public bool TaoPhieuNhap(PhieuNhap pPhieuNhap)
        {
            try
            {
                db.PhieuNhaps.InsertOnSubmit(pPhieuNhap);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool XoaPhieuNhap(string maPhieuNhap,int? soLan)
        {
            try
            {
                PhieuNhap phieuNhapDeleted = db.PhieuNhaps.Where(pn => pn.MaPhieuNhap == maPhieuNhap && pn.SoLan == soLan).Select(pn => pn).FirstOrDefault();
                db.PhieuNhaps.DeleteOnSubmit(phieuNhapDeleted);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public decimal TinhTongTienPhieuNhapTheoKhoangThoiGian(DateTime batDau, DateTime ketThuc)
        {
            return db.PhieuNhaps
                .Where(pn => pn.NgayNhap.Date >= batDau.Date && pn.NgayNhap.Date <= ketThuc.Date)
                .Sum(pn => pn.TongTien);
        }
    }
}
