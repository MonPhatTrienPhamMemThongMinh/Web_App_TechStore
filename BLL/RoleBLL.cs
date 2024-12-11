using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoleBLL
    {
        RoleDAL roleDAL = new RoleDAL();

        public RoleBLL() { }

        public AspNetRole GetRolesByUserId(string userId)
        {
            return roleDAL.GetRoleByUserId(userId);
        }
        public List<AspNetRole> GetAllRole()
        {
            return roleDAL.GetAllRole();
        }
        public bool UpdateLoaiNhanVien(AspNetRole lnv)
        {
            return roleDAL.UpdateLoaiNhanVien(lnv);
        }
        public bool IsTenLoaiNhanVienExit(string tenLNV)
        {
            return roleDAL.IsTenLoaiNhanVienExit(tenLNV);
        }
        public List<AspNetRole> SearchLoaiNhanVien(string lnv)
        {
            return roleDAL.SearchLoaiNhanVien(lnv);
        }
        public bool DeleteLoaiNhanVien(string lnv)
        {
            return roleDAL.DeleteLoaiNhanVien(lnv);
        }
        public bool InsertLoaiNhanVien(AspNetRole lnv)
        {
            return roleDAL.InsertLoaiNhanVien(lnv);
        }

    }
}
