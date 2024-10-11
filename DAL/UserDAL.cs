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

        public UserDAL() {
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
    }
}
