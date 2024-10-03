using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien.Models;

namespace ThuVien.DataAccess
{
    public class CategoryRepository
    {
        private SQLClass sql;
        public CategoryRepository(string cnn) {
            sql = new SQLClass();
            sql.createConnection(cnn);
        }

        public List<Category> GetAllCategories() 
        {
            string query = "SELECT CATEGORYID, CATEGORYNAME FROM CATEGORIES";
            List<Category> categories = new List<Category>();
            DataTable dt = sql.ExecuteQuery(query);
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows) {
                    categories.Add(new Category {
                        CategoryID = row["CATEGORYID"].ToString(),
                        CategoryName = row["CATEGORYName"].ToString()
                    });
                }
            }
            return categories;
        }
    }
}
