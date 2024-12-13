using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class KhuyenMaiDAL
    {
        private DBGAMINGGEARDataContext db;
        public KhuyenMaiDAL()
        {
            this.db = new DBGAMINGGEARDataContext();
        }
        public List<KhuyenMai> LoadDanhSachKhuyenMai()
        {
            try
            {
                return db.KhuyenMais.Select(km => km).OrderByDescending(km => km.maKhuyenMai).ToList();
            }
            catch
            {
                return new List<KhuyenMai>();
            }
        }

        public KhuyenMai LayTTKhuyenMaiTuTenKM(string tenKM)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.FirstOrDefault(km => km.tenKhuyenMai == tenKM);
            if (khuyenMai != null)
            {
                return khuyenMai;
            }
            return null;
        }

        public KhuyenMai LayTTKhuyenMaiTuMaKM(string maKM)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.FirstOrDefault(km => km.maKhuyenMai == maKM);
            if (khuyenMai != null)
            {
                return khuyenMai;
            }
            return null;
        }

        public bool ThemChuongTrinhKhuyenMai(KhuyenMai km)
        {
            try
            {
                db.KhuyenMais.InsertOnSubmit(km);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string TaoMaKhuyenMaiTuDong()
        {
            var khuyenMais = db.KhuyenMais.ToList();
            if (khuyenMais.Any())
            {
                var chuongTrinhKhuyenMaiCuoiCung = khuyenMais.OrderByDescending(km => int.Parse(km.maKhuyenMai.Substring(2))).FirstOrDefault();

                string maKhuyenMaiCuoiCung = chuongTrinhKhuyenMaiCuoiCung.maKhuyenMai;
                int stt = int.Parse(maKhuyenMaiCuoiCung.Substring(2)) + 1;

                return "KM" + stt.ToString("D5");
            }
            return "KM00001";
        }

        public bool CapNhatKhuyenMai(KhuyenMai khuyenMai)
        {
            try
            {
                KhuyenMai updatedKhuyenMai = db.KhuyenMais.FirstOrDefault(km => km.maKhuyenMai == khuyenMai.maKhuyenMai);
                if (updatedKhuyenMai != null)
                {
                    updatedKhuyenMai.tenKhuyenMai = khuyenMai.tenKhuyenMai;
                    updatedKhuyenMai.ngayBatDau = khuyenMai.ngayBatDau;
                    updatedKhuyenMai.ngayKetThuc = khuyenMai.ngayKetThuc;
                    updatedKhuyenMai.moTa = khuyenMai.moTa;
                    updatedKhuyenMai.trangThai = khuyenMai.trangThai;

                    db.SubmitChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi sửa thông tin chương trình khuyến mãi: " + ex.Message, ex);
            }
        }

        public void UpdateTrangThaiKhuyenMai()
        {
            try
            {
                db.ExecuteCommand("EXEC sp_UpdateTrangThaiKhuyenMai");

                db.Refresh(RefreshMode.OverwriteCurrentValues, db.KhuyenMais);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi khi cập nhật trạng thái khuyến mãi: " + ex.Message, ex);
            }
        }

        public bool KiemTraThoiGianKhuyenMaiTruocKhiSua(string maKM, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var sanPhamHienTai = db.KhuyenMaiSanPhams
                                    .Where(kmsp => kmsp.maKhuyenMai == maKM && kmsp.trangThai == "Có hiệu lực")
                                    .Select(kmsp => kmsp.maSanPham)
                                    .ToList();

            // Lấy danh sách khuyến mãi khác có trạng thái "Chưa diễn ra" hoặc "Đang diễn ra"
            var khuyenMaisKhac = db.KhuyenMais
                .Where(km => km.maKhuyenMai != maKM && (km.trangThai == "Chưa diễn ra" || km.trangThai == "Đang diễn ra"))
                .ToList();

            foreach (var km in khuyenMaisKhac)
            {
                var sanPhamKhuyenMaiKhac = db.KhuyenMaiSanPhams
                    .Where(kmsp => kmsp.maKhuyenMai == km.maKhuyenMai && kmsp.trangThai == "Có hiệu lực")
                    .Select(kmsp => kmsp.maSanPham)
                    .ToList();

                // Kiểm tra sản phẩm trùng
                var sanPhamTrung = sanPhamHienTai.Intersect(sanPhamKhuyenMaiKhac).Any();

                if (sanPhamTrung)
                {
                    // Kiểm tra thời gian có bị trùng
                    if (ngayBatDau < km.ngayKetThuc && ngayKetThuc > km.ngayBatDau)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<KhuyenMai> LocDanhSachKhuyenMai(DateTime tgBatDau, DateTime tgKetThuc, string trangThai)
        {
            if (trangThai == "Chọn...")
            {
                return db.KhuyenMais.Where(km => (km.ngayBatDau.Date >= tgBatDau.Date && km.ngayBatDau.Date <= tgKetThuc.Date) ||
                                          (km.ngayKetThuc.Date >= tgBatDau.Date && km.ngayKetThuc.Date <= tgKetThuc.Date))
                            .Select(km => km).OrderByDescending(km => km.maKhuyenMai).ToList();
            }
            else
            {
                return db.KhuyenMais.Where(km => (km.ngayBatDau.Date >= tgBatDau.Date && km.ngayBatDau.Date <= tgKetThuc.Date) ||
                                          (km.ngayKetThuc.Date >= tgBatDau.Date && km.ngayKetThuc.Date <= tgKetThuc.Date) &&
                                          km.trangThai == trangThai)
                                        .Select(km => km).OrderByDescending(km => km.maKhuyenMai).ToList();
            }
        }
    }
}
