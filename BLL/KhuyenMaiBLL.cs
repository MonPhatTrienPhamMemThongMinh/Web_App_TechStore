using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class KhuyenMaiBLL
    {
        private KhuyenMaiDAL kmdal;
        public KhuyenMaiBLL()
        {
            this.kmdal= new KhuyenMaiDAL();
        }
        public List<KhuyenMai> LoadDanhSachKhuyenMai()
        {
            return kmdal.LoadDanhSachKhuyenMai();
        }

        public KhuyenMai LayTTKhuyenMaiTuTenKM(string tenKM)
        {
            return kmdal.LayTTKhuyenMaiTuTenKM(tenKM);
        }

        public KhuyenMai LayTTKhuyenMaiTuMaKM(string maKM)
        {
            return kmdal.LayTTKhuyenMaiTuMaKM(maKM);
        }

        public bool ThemChuongTrinhKhuyenMai(KhuyenMai km)
        {
            return kmdal.ThemChuongTrinhKhuyenMai(km);
        }

        public string TaoMaKhuyenMaiTuDong()
        {
            return kmdal.TaoMaKhuyenMaiTuDong();
        }

        public bool CapNhatKhuyenMai(KhuyenMai khuyenMai)
        {
            return kmdal.CapNhatKhuyenMai(khuyenMai);
        }

        public void UpdateTrangThaiKhuyenMai()
        {
            kmdal.UpdateTrangThaiKhuyenMai();
        }

        public bool KiemTraThoiGianKhuyenMaiTruocKhiSua(string maKM, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            return kmdal.KiemTraThoiGianKhuyenMaiTruocKhiSua(maKM, ngayBatDau, ngayKetThuc);
        }

        public List<KhuyenMai> LocDanhSachKhuyenMai(DateTime tgBatDau, DateTime tgKetThuc, string trangThai)
        {
            return kmdal.LocDanhSachKhuyenMai(tgBatDau, tgKetThuc, trangThai);
        }
    }
}
