using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class ChiTietPhieuDatBLL
    {
        private ChiTietPhieuDatDAL chiTietPhieuDatDAL;
        public ChiTietPhieuDatBLL()
        {
            this.chiTietPhieuDatDAL = new ChiTietPhieuDatDAL();
        }
        public List<ChiTietPhieuDat> LayChiTietPhieuDat(string maPhieuDat)
        {
            return chiTietPhieuDatDAL.LayChiTietPhieuDat(maPhieuDat);
        }
        public bool KiemTraTonTaiChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            return chiTietPhieuDatDAL.KiemTraTonTaiChiTietPhieuDat(pChiTietPhieuDat);
        }
        public bool ThemChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            return chiTietPhieuDatDAL.TaoChiTietPhieuDat(pChiTietPhieuDat);
        }
        public bool SuaChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            return chiTietPhieuDatDAL.SuaChiTietPhieuDat(pChiTietPhieuDat);
        }
        public bool XoaChiTietPhieuDat(ChiTietPhieuDat pChiTietPhieuDat)
        {
            return chiTietPhieuDatDAL.XoaChiTietPhieuDat(pChiTietPhieuDat);
        }
        public bool XoaChiTietPhieuDat(string maPhieuDat,string maSanPham)
        {
            return chiTietPhieuDatDAL.XoaChiTietPhieuDat(maPhieuDat,maSanPham);
        }
    }
}
