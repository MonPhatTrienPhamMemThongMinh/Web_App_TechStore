using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
