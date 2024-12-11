using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class ChiTietPhieuNhapBLL
    {
        private ChiTietPhieuNhapDAL chiTietPhieuNhapDAL;
        public ChiTietPhieuNhapBLL() 
        {
            this.chiTietPhieuNhapDAL = new ChiTietPhieuNhapDAL();
        }
        public List<ChiTietPhieuNhap> LayDanhSachChiTietPhieuNhap(string maPhieuNhap)
        {
            return chiTietPhieuNhapDAL.LayDanhSachChiTietPhieuNhap(maPhieuNhap);
        }
        public bool TaoChiTietPhieuNhap(ChiTietPhieuNhap pChiTietPhieuNhap)
        {
            return chiTietPhieuNhapDAL.TaoChiTietPhieuNhap(pChiTietPhieuNhap);
        }
        public bool SuaChiTietPhieuNhap(ChiTietPhieuNhap pChiTietPhieuNhap)
        {
            return chiTietPhieuNhapDAL.SuaChiTietPhieuNhap(pChiTietPhieuNhap);
        }
    }
}
