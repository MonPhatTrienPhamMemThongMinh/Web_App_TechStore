using System;
using System.Collections.Generic;
using System.Data;
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

        public List<AspNetRole> GetRoleByUserId(string userId)
        {
            var roles = (from r in db.AspNetRoles
                         join ur in db.AspNetUserRoles on r.Id equals ur.RoleId
                         where ur.UserId == userId
                         select r).ToList();

            return roles;
        }
    }
}
