using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien.Models;

namespace ThuVien.DataAccess
{
    public class RoleRepository
    {
        SQLClass sql;
        public RoleRepository(string cnn) {
            sql = new SQLClass();
            sql.createConnection(cnn);
        }

        public List<Role> GetRoleByUserId(string userId)
        {
            List<Role> roles = new List<Role>();

            string query = @"
                SELECT r.Id, r.Name 
                FROM AspNetRoles r
                INNER JOIN AspNetUserRoles ur ON r.Id = ur.RoleId
                WHERE ur.UserId = @UserId";

            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId }
            };

            DataTable dt = sql.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    roles.Add(new Role
                    {
                        RoleId = row["ID"].ToString(),
                        RoleName = row["Name"].ToString(),
                    });
                }
            }

            return roles;
        }
    }
}
