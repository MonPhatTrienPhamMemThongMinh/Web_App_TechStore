using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien.Models;

namespace ThuVien.DataAccess
{
    public class ProductRepository
    {
        private SQLClass sql;
        public ProductRepository(string cnn)
        {
            sql = new SQLClass();
            sql.createConnection(cnn);
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string query = "SELECT p.ProductID, p.ProductDescription, p.BrandID, p.CategoryID, " +
                            "p.AvailabilityStatus, p.Quantity, p.BaoHanh, p.ProductName, " + 
                            "p.ProductPic, p.Price, b.BrandName, c.CategoryName FROM PRODUCTS p " +
                            "INNER JOIN BRANDS b ON p.BRANDID = b.BRANDID " +
                            "INNER JOIN CATEGORIES c ON p.CATEGORYID = c.CATEGORYID";
            DataTable dt = sql.ExecuteQuery(query);
            
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows )
                {
                    products.Add(new Product()
                    {
                        ProductID = row["ProductID"].ToString(),
                        ProductDescription = row["ProductDescription"].ToString(),
                        BrandID = row["BrandID"].ToString(),
                        CategoryID = row["CategoryID"].ToString(),
                        AvailabilityStatus = row["AvailabilityStatus"].ToString(),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        BaoHanh = row["BaoHanh"].ToString(),
                        ProductName = row["ProductName"].ToString(),
                        ProductPic = row["ProductPic"].ToString(),
                        Price = Convert.ToInt32(row["Price"]),
                        BrandName = row["BrandName"].ToString(),
                        CategoryName = row["CategoryName"].ToString()
                    });
                }
            }
            return products;
        }

        public Product GetProductById (string productID)
        {
            string query = "SELECT p.ProductID, p.ProductDescription, p.BrandID, p.CategoryID, " +
                            "p.AvailabilityStatus, p.Quantity, p.BaoHanh, p.ProductName, " +
                            "p.ProductPic, p.Price, b.BrandName, c.CategoryName FROM PRODUCTS p " +
                "INNER JOIN BRANDS b ON p.BRANDID = b.BRANDID " + 
                "INNER JOIN CATEGORIES c ON p.CATEGORYID = c.CATEGORYID " +
                "WHERE p.PRODUCTID = @PRODUCTID";

            var parameters = new Dictionary<string, object>
            {
                {
                    "@PRODUCTID", productID
                } 
            };

            DataTable dt = sql.ExecuteQuery(query, parameters);
            if(dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Product
                {
                    ProductID = row["ProductID"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    ProductPic = row["ProductPic"].ToString(),
                    Price = Convert.ToInt32(row["Price"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    BaoHanh = row["BaoHanh"].ToString(),
                    BrandName = row["BrandName"].ToString(),
                    CategoryName = row["CategoryName"].ToString(),
                    ProductDescription = row["ProductDescription"].ToString(),
                    AvailabilityStatus = row["AvailabilityStatus"].ToString(),
                    BrandID = row["BrandID"].ToString(),
                    CategoryID = row["CategoryID"].ToString()
                };
            }
            return null;
        }

        public bool AddProduct(Product product)
        {
            string query = "INSERT INTO Products (PRODUCTID, PRODUCTNAME, PRICE, QUANTITY, BRANDID, CATEGORYID, BaoHanh, ProductDescription, AvailabilityStatus, ProductPic) " +
                "VALUES (@ProductID, @ProductName, @Price, @Quantity, @BrandID, @CategoryID, @BaoHanh, @ProductDescription, @AvailabilityStatus, @ProductPic)";

            var parameters = new Dictionary<string, object>
            {
                { "@ProductID", product.ProductID },
                { "@ProductDescription", product.ProductDescription },
                { "@BrandID", product.BrandID },
                { "@CategoryID", product.CategoryID },
                { "@AvailabilityStatus", product.AvailabilityStatus },
                { "@Quantity", product.Quantity },
                { "@BaoHanh", product.BaoHanh },
                { "@ProductName", product.ProductName },
                { "@Price", product.Price },
                { "@ProductPic", product.ProductPic }
            };

            try
            {
                int rowsAffected = sql.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0; // Trả về true nếu thêm thành công
            }
            catch (Exception ex){
                throw new ApplicationException("Lỗi thêm sản phẩm: " + ex.Message, ex);
            }
        }

        public bool DeleteProduct(Product product)
        {
            string query = "Delete From Products WHERE ProductID = @ProductID";
            var parameters = new Dictionary<string, object>
            {
                { "@ProductID", product.ProductID}
            };

            try
            {
                int rowsAffected = sql.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi xóa sản phẩm: " + ex.Message, ex);
            }
        }

        public bool UpdateProduct(Product product)
        {
            string query = @"UPDATE Products SET 
                                    ProductName = @ProductName,
                                    Price = @Price,
                                    Quantity = @Quantity,
                                    ProductDescription = @ProductDescription,
                                    BrandID = @BrandID,
                                    CategoryID = @CategoryID,
                                    AvailabilityStatus = @AvailabilityStatus,
                                    BaoHanh = @BaoHanh,
                                    ProductPic = @ProductPic
                                 WHERE ProductID = @ProductID";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@ProductName", product.ProductName },
                { "@Price", product.Price },
                { "@Quantity", product.Quantity },
                { "@ProductDescription", product.ProductDescription },
                { "@BrandID", product.BrandID },
                { "@CategoryID", product.CategoryID },
                { "@AvailabilityStatus", product.AvailabilityStatus },
                { "@BaoHanh", product.BaoHanh },
                { "@ProductPic", product.ProductPic },
                { "@ProductID", product.ProductID }
            };

            try
            {
                // Thực thi câu lệnh SQL
                int rowsAffected = sql.ExecuteNonQuery(query, parameters);

                // Nếu số dòng bị ảnh hưởng > 0 thì cập nhật thành công
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi sửa sản phẩm: " + ex.Message, ex);
            }
        }
    }
}
