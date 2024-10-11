using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien;

namespace BLL
{
    public class RoleBLL
    {
        RoleDAL roleDAL = new RoleDAL();

        public RoleBLL() { }

        public List<AspNetRole> GetRolesByUserId(string userId)
        {
            return roleDAL.GetRoleByUserId(userId);
        }
    }
}
