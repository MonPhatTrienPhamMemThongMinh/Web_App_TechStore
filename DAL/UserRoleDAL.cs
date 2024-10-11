using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien
{
    public class UserRoleDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public UserRoleDAL() { }

        public AspNetUserRole GetUserRoleById(string userId)
        {
            try
            {
                AspNetUserRole userrole = db.AspNetUserRoles.FirstOrDefault(ur => ur.UserId == userId);
                return userrole;
            }
            catch {
                return null;
            }
        }
    }
}
