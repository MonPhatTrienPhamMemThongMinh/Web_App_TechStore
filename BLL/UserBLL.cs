using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL
    {
        UserDAL udal = new UserDAL();
        public UserBLL() { }

        public AspNetUser GetUserByUsername(string username)
        {
            return udal.GetUserByUsername(username);
        }
        public string MaHoaMatKhauKieuSha256Hash(string pass)
        {
            return udal.MaHoaMatKhauKieuSha256Hash(pass);
        }
        public List<AspNetUser> LoadAllUsers()
        {
            return udal.LoadAllUsers();
        }
        public int DemSoNhanVienThuocLoai(string maLoaiNhanVien)
        {
            return udal.DemSoNhanVienThuocLoai(maLoaiNhanVien);
        }
        public bool IsTaiKhoanDuplicate(string tk)
        {
            return udal.IsTaiKhoanDuplicate(tk);
        }
        public bool IsSDTDuplicate(string sdt)
        {
            return udal.IsSDTDuplicate(sdt);
        }
        public bool InsertNhanVien(AspNetUser nv)
        {
            return udal.InsertNhanVien(nv);
        }
        public bool DeleteNhanVien(string nv)
        {
            return udal.DeleteNhanVien(nv);
        }
        public bool UpdateNhanVien(AspNetUser nv)
        {
            return udal.UpdateNhanVien(nv);
        }
        public List<AspNetUser> SearchNhanVien(string nv)
        {
            return udal.SearchNhanVien(nv);
        }
    }
}
