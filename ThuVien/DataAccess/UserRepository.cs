using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien.Models;

namespace ThuVien
{
    public class UserRepository
    {
        private SQLClass sql;

        public UserRepository(string connectString) { 
            sql = new SQLClass();
            sql.createConnection(connectString);
        }

        public Users GetUserByUsername(string username)
        {
            string query = "SELECT Id, UserName, PasswordHash FROM AspNetUsers WHERE Username = '" + username + "'";
            DataTable dt = new DataTable();
            dt = sql.ExecuteQuery(query);
            if(dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Users
                {
                    UserId = row["Id"].ToString(),
                    UserName = row["UserName"].ToString(),
                    PasswordHash = row["PasswordHash"].ToString()
                };
            }
            return null;
        }
    }
}
