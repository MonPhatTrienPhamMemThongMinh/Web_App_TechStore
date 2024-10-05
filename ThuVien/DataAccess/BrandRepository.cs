using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien.Models;

namespace ThuVien.DataAccess
{
    public class BrandRepository
    {
        private SQLClass sql;
        public BrandRepository(string _cnn)
        {
            sql = new SQLClass();
            sql.createConnection(_cnn);
        }

        public List<Brand> GetAllBrands()
        {
            string query = "SELECT BRANDID, BRANDNAME, BrandDescription, BrandPic, BrandBackground FROM BRANDS";
            List<Brand> brands = new List<Brand>();
            DataTable dt = sql.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    brands.Add(new Brand
                    {
                        BrandID = row["BRANDID"].ToString(),
                        BrandName = row["BRANDNAME"].ToString(),
                        BrandDescription = row["BrandDescription"].ToString(),
                        BrandPic = row["BrandPic"].ToString(),
                        BrandBackground = row["BrandBackground"].ToString()
                    });
                }
            }
            return brands;
        }

        public Brand GetBrandById(string brandId)
        {
            string query = "SELECT BrandID, BrandName, BrandDescription, BrandPic, BrandBackground " +
                "FROM Brands where BrandID = @BrandID";

            var p = new Dictionary<string, object>
            {
                {
                    "@BrandID", brandId
                }
            };

            DataTable dt = sql.ExecuteQuery(query, p);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Brand
                {
                    BrandID = row["BrandID"].ToString(),
                    BrandName = row["BrandName"].ToString(),
                    BrandPic = row["BrandPic"].ToString(),
                    BrandDescription = row["BrandDescription"].ToString(),
                    BrandBackground = row["BrandBackground"].ToString()
                };
            }
            return null;
        }
    }
}
