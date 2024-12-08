using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UserRoleBLL
    {
        UserRoleDAL urdal = new UserRoleDAL();
        public UserRoleBLL() { }

        public AspNetUserRole GetUserRoleById(string userId)
        {
            return urdal.GetUserRoleById(userId);
        }
    }
}
