using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class KhuyenMaiSanPhamDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public KhuyenMaiSanPhamDAL() { }
        public List<KhuyenMaiSanPham> LoadSanPhamTheoKhuyenMai(string maKhuyenMai)
        {
            try
            {
                var listSp = db.KhuyenMaiSanPhams
                                        .Where(kmsp => kmsp.maKhuyenMai == maKhuyenMai && kmsp.trangThai != "Đã hủy")
                                        .Select(sp => sp)
                                        .ToList();
                foreach (var spkm in listSp)
                {
                    spkm.tenSanPham = db.Products.Where(sp => sp.ProductID == spkm.maSanPham).Select(sp => sp.ProductName).FirstOrDefault();
                }
                return listSp;
            }
            catch
            {
                return new List<KhuyenMaiSanPham>();
            }
        }

        public bool ThemHoacCapNhatSanPhamVaoCTKhuyenMai(KhuyenMaiSanPham kmspham)
        {
            try
            {
                var sanPhamKM = db.KhuyenMaiSanPhams.FirstOrDefault(kmsp => kmsp.maKhuyenMai == kmspham.maKhuyenMai
                                                                && kmsp.maSanPham == kmspham.maSanPham);
                if (sanPhamKM == null)
                {
                    db.KhuyenMaiSanPhams.InsertOnSubmit(kmspham);
                }
                else
                {
                    sanPhamKM.phanTramGiam = kmspham.phanTramGiam;
                    sanPhamKM.trangThai = kmspham.trangThai;
                }

                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<KhuyenMaiSanPham> LayKhuyenMaiTheoSanPham(string maSanPham)
        {
            var khuyenMaiSanPhams = db.KhuyenMaiSanPhams
                .Where(kmsp => kmsp.maSanPham == maSanPham && kmsp.trangThai == "Có hiệu lực")
                .ToList();

            return khuyenMaiSanPhams;
        }

        public KhuyenMaiSanPham LayTTSanPhamCuaKhuyenMai(string maKhuyenMai, string maSanPham)
        {
            var khuyenMaiSanPham = db.KhuyenMaiSanPhams
                                    .Where(kmsp => kmsp.maSanPham == maSanPham && kmsp.maKhuyenMai == maKhuyenMai && kmsp.trangThai == "Có hiệu lực")
                                    .Select(kmsp => kmsp)
                                    .FirstOrDefault();
            if (khuyenMaiSanPham != null)
            {
                return khuyenMaiSanPham;
            }
            return null;
        }

        // Kiểm tra xem sản phẩm có thuộc khuyến mãi nào trùng thời gian không
        public bool KiemTraSanPhamTrongKhoangThoiGianKhuyenMai(string maKM, string maSanPham)
        {
            var khuyenMaiMoi = db.KhuyenMais.FirstOrDefault(km => km.maKhuyenMai == maKM);
            if (khuyenMaiMoi == null)
            {
                return false;
            }
            // Kiểm tra có khuyến mãi khác trùng thời gian
            var khuyenMaiTrung = db.KhuyenMaiSanPhams
                .Where(kmsp => kmsp.maSanPham == maSanPham
                            && kmsp.maKhuyenMai != maKM
                            && db.KhuyenMais.Any(km =>
                                km.maKhuyenMai == kmsp.maKhuyenMai &&
                                km.ngayBatDau < khuyenMaiMoi.ngayKetThuc &&
                                km.ngayKetThuc > khuyenMaiMoi.ngayBatDau && km.trangThai != "Đã kết thúc"))
                .FirstOrDefault();

            return khuyenMaiTrung == null; // Trả về true nếu không có khuyến mãi trùng
        }

        public bool CapNhatTrangThaiSanPhamTrongKhuyenMai(string maKhuyenMai, string trangThai)
        {
            try
            {
                var sanPhamKM = db.KhuyenMaiSanPhams.Where(kmsp => kmsp.maKhuyenMai == maKhuyenMai);
                foreach (var sp in sanPhamKM)
                {
                    sp.trangThai = trangThai == "Có hiệu lực" ? "Có hiệu lực" : "Hết hiệu lực";
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public KhuyenMaiSanPham TimKhuyenMaiSanPhamTheoNgayLapHoaDon(string maSanPham, DateTime ngayKhuyenMai)
        {
            List<KhuyenMai> danhSachKhuyenMai = db.KhuyenMais.Where(km => km.ngayBatDau <= ngayKhuyenMai && km.ngayKetThuc >= ngayKhuyenMai).ToList();
            foreach (KhuyenMai item in danhSachKhuyenMai)
            {
                List<KhuyenMaiSanPham> danhSachSanPhamKhuyenMai = db.KhuyenMaiSanPhams.Where(kmsp => kmsp.maKhuyenMai == item.maKhuyenMai).ToList();
                foreach (KhuyenMaiSanPham sp in danhSachSanPhamKhuyenMai)
                {
                    if (sp.maSanPham == maSanPham)
                    {
                        return sp;
                    }
                }
            }
            return null;
        }
    }
}
