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
    }
}
