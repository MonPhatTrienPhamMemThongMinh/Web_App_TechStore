using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class KhuyenMaiSanPhamBLL
    {
        KhuyenMaiSanPhamDAL kmspdal = new KhuyenMaiSanPhamDAL();
        public KhuyenMaiSanPhamBLL() { }

        public List<KhuyenMaiSanPham> LoadSanPhamTheoKhuyenMai(string maKhuyenMai)
        {
            return kmspdal.LoadSanPhamTheoKhuyenMai(maKhuyenMai);
        }

        public bool ThemHoacCapNhatSanPhamVaoCTKhuyenMai(KhuyenMaiSanPham kmsp)
        {
            return kmspdal.ThemHoacCapNhatSanPhamVaoCTKhuyenMai(kmsp);
        }
        public List<KhuyenMaiSanPham> LayKhuyenMaiTheoSanPham(string maSanPham)
        {
            return kmspdal.LayKhuyenMaiTheoSanPham(maSanPham);
        }

        public KhuyenMaiSanPham LayTTSanPhamCuaKhuyenMai(string maKhuyenMai, string maSanPham)
        {
            return kmspdal.LayTTSanPhamCuaKhuyenMai(maKhuyenMai, maSanPham);
        }

        public bool KiemTraSanPhamTrongKhoangThoiGianKhuyenMai(string maKM, string maSanPham)
        {
            return kmspdal.KiemTraSanPhamTrongKhoangThoiGianKhuyenMai(maKM, maSanPham);
        }

        public bool CapNhatTrangThaiSanPhamTrongKhuyenMai(string maKhuyenMai, string trangThai)
        {
            return kmspdal.CapNhatTrangThaiSanPhamTrongKhuyenMai(maKhuyenMai, trangThai);
        }
        public KhuyenMaiSanPham TimKhuyenMaiSanPhamTheoNgayLapHoaDon(string maSanPham, DateTime ngayKhuyenMai)
        {
            return kmspdal.TimKhuyenMaiSanPhamTheoNgayLapHoaDon(maSanPham, ngayKhuyenMai);
        }
    }
}
