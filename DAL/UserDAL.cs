using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DTO;
using System.CodeDom;
using System.Web.Helpers;
namespace DAL
{
    public class UserDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public UserDAL()
        {
        }
        public AspNetUser LayThongTinTheoMa(string ma)
        {
            try
            {
                AspNetUser khachHang = db.AspNetUsers.FirstOrDefault(kh => kh.Id == ma);                
                return khachHang;
            }
            catch
            {
                return null;
            }
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
        public string MaHoaMKMoi(string mkmoi)
        {
            var hashedPassword = MaHoaMatKhauKieuSha256Hash(mkmoi);
            return hashedPassword;
        }        
        public string MaHoaMatKhauKieuSha256Hash(string pass)
        {
            return Crypto.HashPassword(pass);
            //var password = new PasswordHasher();
            //return password.HashPassword(pass);
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
        public bool InsertNhanVien(AspNetUser nv)
        {
            try
            {
                // Thêm nhân viên vào cơ sở dữ liệu
                nv.Id = Guid.NewGuid().ToString();
                nv.SecurityStamp = Guid.NewGuid().ToString();
                db.AspNetUsers.InsertOnSubmit(nv);                
                db.SubmitChanges();
                var loaiTKID = db.AspNetRoles.Where(role => role.Name == "Customer").Select(role => role.Id).FirstOrDefault();
                var idTK = db.AspNetUsers.Where(user => user.UserName == nv.UserName).Select(user => user.Id).FirstOrDefault();
                UserRoleDAL userRoleDAL = new UserRoleDAL();
                userRoleDAL.InsertUserRole(idTK, loaiTKID);
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
        public bool UpdateMatKhauMoi(AspNetUser nv)
        {
            try
            {
                var exnv = db.AspNetUsers.SingleOrDefault(n => n.Id == nv.Id);
                if (exnv != null)
                {
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
