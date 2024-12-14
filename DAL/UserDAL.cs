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
    public class UserDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();

        public UserDAL()
        {
        }
        public AspNetUser GetUserByUsername(string username)
        {
            try
            {
                return db.AspNetUsers.FirstOrDefault(u => u.UserName == username);
            }
            catch
            {
                return null;
            }
        }
        public List<AspNetUser> LoadAllUsers()
        {
            try
            {
                var customerRoleId = db.AspNetRoles.Where(r => r.Name == "Customer").Select(r => r.Id).FirstOrDefault();
                if (customerRoleId == null)
                {
                    return new List<AspNetUser>();
                }

                var users = (from user in db.AspNetUsers
                             join userRole in db.AspNetUserRoles on user.Id equals userRole.UserId
                             where userRole.RoleId == customerRoleId
                             select user).ToList();

                return users;
            }
            catch
            {
                return new List<AspNetUser>();
            }
        }
        public string MaHoaMatKhauKieuSha256Hash(string pass)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass)); // chuyển pass thành mảng byte
                return string.Concat(bytes.Select(b => b.ToString("x2"))); // chuyển sang chuỗi hexa 
            }
        }
        public int DemSoNhanVienThuocLoai(string maLoaiNhanVien)
        {
            return db.AspNetUsers.Where(nv => nv.Id == maLoaiNhanVien).Count();
        }
        public bool IsTaiKhoanDuplicate(string tk)
        {

            if (string.IsNullOrWhiteSpace(tk))
            {
                return false;
            }

            // Chuẩn hóa dữ liệu: loại bỏ khoảng trắng
            string normalizedTk = tk.Trim();

            // Kiểm tra trùng lặp tên đăng nhập, phân biệt hoa-thường
            return LoadAllUsers().Any(nv => nv.UserName.Equals(tk, StringComparison.Ordinal));
        }
        public bool IsSDTDuplicate(string sdt)
        {
            return LoadAllUsers().Any(nv => nv.PhoneNumber.Equals(sdt, StringComparison.OrdinalIgnoreCase));
        }
        public string GenerateNewEmployeeCode()
        {
            // Tìm mã nhân viên lớn nhất trong bảng
            var maxMaNV = db.AspNetUsers
                .Where(x => x.Id.StartsWith("NV"))
                .Select(x => x.Id)
                .ToList(); // Chuyển đổi thành danh sách

            // Nếu không có mã nào, bắt đầu từ "NV0001"
            var newMaNV = "NV0001"; // Mã khởi tạo mặc định
            if (maxMaNV.Count > 0)
            {
                // Lấy mã lớn nhất
                var maxCode = maxMaNV.Max();

                // Kiểm tra xem mã nhân viên lớn nhất có vượt quá NV9999 không
                var numericPart = int.Parse(maxCode.Substring(2)); // Lấy phần số (bỏ 2 ký tự "NV")
                if (numericPart >= 9999)
                {
                    // Nếu đã đạt đến giới hạn NV9999, thông báo lỗi
                    throw new Exception("Số lượng nhân viên đã đạt đến giới hạn NV9999.");
                }

                // Tạo mã mới với 4 chữ số
                newMaNV = "NV" + (numericPart + 1).ToString("D4");
            }

            return newMaNV;
        }
        public bool InsertNhanVien(AspNetUser nv)
        {
            try
            {
                // Sinh mã nhân viên mới
                nv.Id = GenerateNewEmployeeCode();

                // Thêm nhân viên vào cơ sở dữ liệu
                db.AspNetUsers.InsertOnSubmit(nv);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi vào log hoặc thông báo
                Debug.WriteLine("Thêm thất bại: " + ex.Message);
                throw new Exception("Thêm thất bại: " + ex.Message);
            }
        }
        public bool DeleteNhanVien(string manv)
        {
            try
            {                
                //bool existsInPhieuDat = db.PhieuDats.Any(pd => pd.UserID == manv);
                //bool existsInPhieuNhap = db.PhieuNhaps.Any(pn => pn.UserID == manv);

                //if (existsInPhieuDat || existsInPhieuNhap)
                //{
                //    return false;
                //}
                var nv = db.AspNetUsers.FirstOrDefault(k => k.Id == manv);
                if (nv != null)
                {
                    db.AspNetUsers.DeleteOnSubmit(nv);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Xóa nhân viên thất bại: " + ex.Message);
                return false;
            }           
        }
        public bool UpdateNhanVien(AspNetUser nv)
        {
            try
            {
                var exnv = db.AspNetUsers.FirstOrDefault(n => n.Id == nv.Id);
                if (exnv != null)
                {
                    exnv.FullName = nv.FullName;
                    exnv.Address = nv.Address;
                    exnv.Birthday = nv.Birthday;
                    exnv.PhoneNumber = nv.PhoneNumber;
                    exnv.UserName = nv.UserName;
                    exnv.PasswordHash = nv.PasswordHash;
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
        public List<AspNetUser> SearchNhanVien(string keyword)
        {
            // Kiểm tra từ khóa có phải null hay không
            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu không có từ khóa, trả về tất cả loại nhân viên
                return LoadAllUsers();
            }

            // Tìm kiếm nhân viên 
            var filteredNhanVien = LoadAllUsers().Where(nv => (nv.FullName != null && nv.FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                     (nv.PhoneNumber != null && nv.PhoneNumber.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||                    
                     (nv.UserName != null && nv.UserName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();

            return filteredNhanVien;
        }
    }
}
