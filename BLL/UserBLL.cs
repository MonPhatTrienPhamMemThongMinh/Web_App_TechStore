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

        public List<AspNetUser> LoadAllUsers()
        {
            return udal.LoadAllUsers();
        }
    }
}
