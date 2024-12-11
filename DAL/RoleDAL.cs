using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class RoleDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public RoleDAL() {

        }
        public AspNetRole GetRoleByUserId(string userId)
        {
            var roles = (from r in db.AspNetRoles
                         join ur in db.AspNetUserRoles on r.Id equals ur.RoleId
                         where ur.UserId == userId
                         select r).FirstOrDefault();
            return roles;
        }
        public List<AspNetRole> GetAllRole()
        {
            var roles = (from r in db.AspNetRoles
                         join ur in db.AspNetUserRoles on r.Id equals ur.RoleId
                         select r).ToList();
            return roles;
        }
        public bool IsTenLoaiNhanVienExit(string tenLNV)
        {
            var ex = db.AspNetRoles.Any(lnv => lnv.Name == tenLNV);
            return ex;
        }
        public bool UpdateLoaiNhanVien(AspNetRole lnv)
        {
            try
            {
                if (IsTenLoaiNhanVienExit(lnv.Name))
                {
                    throw new InvalidOperationException("Tên loại nhân viên đã tồn tại");
                }
                var exlnv = db.AspNetRoles.FirstOrDefault(n => n.Id == lnv.Id);
                if (exlnv != null)
                {
                    exlnv.Name = lnv.Name;
                    db.SubmitChanges();
                    return true;
                }
                return false; // Không tìm thấy để sửa
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<AspNetRole> SearchLoaiNhanVien(string keyword)
        {
            // Kiểm tra từ khóa có phải null hay không
            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu không có từ khóa, trả về tất cả loại nhân viên
                return GetAllRole();
            }

            // Tìm kiếm các loại nhân viên 
            var filteredLoaiNV = GetAllRole()
                .Where(x => x.Name != null &&
                            x.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            return filteredLoaiNV;
        }
        public bool DeleteLoaiNhanVien(string malnv)
        {
            try
            {      
                var lnv = db.AspNetRoles.FirstOrDefault(k => k.Id == malnv);
                if (lnv != null)
                {
                    db.AspNetRoles.DeleteOnSubmit(lnv);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Xóa loại nhân viên thất bại: " + ex.Message);
                return false;
            }
        }
        public bool InsertLoaiNhanVien(AspNetRole lnv)
        {
            try
            {
                if (IsTenLoaiNhanVienExit(lnv.Name))
                {
                    throw new InvalidOperationException("Tên loại nhân viên đã tồn tại");
                }
                // Tạo mã loại nhân viên mới
                string newMaLoaiNV = GenerateNewMaLoaiNV();

                // Gán mã loại nhân viên mới
                lnv.Name = newMaLoaiNV;

                // Thêm loại nhân viên vào cơ sở dữ liệu
                db.AspNetRoles.InsertOnSubmit(lnv);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Thêm thất bại: " + ex.Message);
                throw new Exception("Thêm thất bại: " + ex.Message);
            }
        }        
        private string GenerateNewMaLoaiNV()
        {
            // Lấy danh sách mã loại nhân viên có tiền tố "LNV" trong bảng
            var maxMaLoaiNV = db.AspNetRoles
                .Where(x => x.Id.StartsWith("LNV"))
                .Select(x => x.Id)
                .ToList();
            // Thiết lập mã mặc định là "LNV001"
            var newMaLoaiNV = "LNV001";

            if (maxMaLoaiNV.Count > 0)
            {
                // Lấy mã lớn nhất hiện có trong danh sách
                var maxCode = maxMaLoaiNV.Max();

                // Chuyển phần số của mã loại nhân viên thành số nguyên và tăng lên 1
                var numericPart = int.Parse(maxCode.Substring(3)); // Bỏ "LNV" và lấy phần số
                newMaLoaiNV = "LNV" + (numericPart + 1).ToString("D3"); // Tạo mã mới với 3 chữ số
            }

            return newMaLoaiNV;
        }
    }
}
