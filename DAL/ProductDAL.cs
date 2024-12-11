using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ProductDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public ProductDAL()
        {

        }

        public List<Product> GetAllProducts()
        {
            var products = (from p in db.Products
                            join b in db.Brands on p.BrandID equals b.BrandID
                            join c in db.Categories on p.CategoryID equals c.CategoryID
                            join ncc in db.NhaCungCaps on p.maNhaCungCap equals ncc.maNhaCungCap
                            select p).ToList();
            foreach (var p in products)
            {
                var brand = db.Brands.FirstOrDefault(b => b.BrandID == p.BrandID);
                if (brand != null)
                {
                    p.BrandName = brand.BrandName;
                }

                var category = db.Categories.FirstOrDefault(c => c.CategoryID == p.CategoryID);
                if (category != null)
                {
                    p.CategoryName = category.CategoryName;
                }

                var supplier = db.NhaCungCaps.FirstOrDefault(ncc => ncc.maNhaCungCap == p.maNhaCungCap);
                if (supplier != null)
                {
                    p.SupplierName = supplier.tenNhaCungCap;
                }
            }
            return products;
        }

        public Product GetProductById(string productID)
        {
            try
            {
                Product product = db.Products.FirstOrDefault(p => p.ProductID == productID);
                return product;
            }
            catch
            {
                return null;
            }
        }

        public bool AddProduct(Product product)
        {
            try
            {
                db.Products.InsertOnSubmit(product);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(Product product)
        {
            try
            {
                Product productToDelete = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                if (productToDelete != null)
                {
                    db.Products.DeleteOnSubmit(productToDelete);
                    db.SubmitChanges();
                    return true;
                }

                return false;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Lỗi xóa sản phẩm: " + ex.Message, ex);
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                Product productToUpdate = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                if (productToUpdate != null)
                {
                    productToUpdate.ProductName = product.ProductName;
                    productToUpdate.ProductDescription = product.ProductDescription;
                    productToUpdate.BrandID = product.BrandID;
                    productToUpdate.CategoryID = product.CategoryID;
                    productToUpdate.AvailabilityStatus = product.AvailabilityStatus;
                    productToUpdate.BaoHanh = product.BaoHanh;
                    productToUpdate.ProductPic = product.ProductPic;
                    productToUpdate.maNhaCungCap = product.maNhaCungCap;

                    // Lưu thay đổi
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi sửa sản phẩm: " + ex.Message, ex);
            }
        }

        public static string RemoveVietnameseDaus(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string[] daus = new string[]
            {
        "aáàảãạăắằẳẵặâấầẩẫậ", "AÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ",
        "dđ", "DĐ",
        "eéèẻẽẹêếềểễệ", "EÉÈẺẼẸÊẾỀỂỄỆ",
        "iíìỉĩị", "IÍÌỈĨỊ",
        "oóòỏõọôốồổỗộơớờởỡợ", "OÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ",
        "uúùủũụưứừửữự", "UÚÙỦŨỤƯỨỪỬỮỰ",
        "yýỳỷỹỵ", "YÝỲỶỸỴ"
            };

            foreach (var dau in daus)
            {
                foreach (var ch in dau.Skip(1))
                {
                    input = input.Replace(ch, dau[0]);
                }
            }

            return input;
        }

        public List<Product> SearchProducts(string searchItem)
        {
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                return db.Products.ToList();
            }

            // Chuẩn hóa chuỗi tìm kiếm
            string normalizedSearchItem = RemoveVietnameseDaus(searchItem.ToLower());

            var filteredProduct = (from p in db.Products
                                   join b in db.Brands on p.BrandID equals b.BrandID
                                   join c in db.Categories on p.CategoryID equals c.CategoryID
                                   join ncc in db.NhaCungCaps on p.maNhaCungCap equals ncc.maNhaCungCap
                                   select new
                                   {
                                       Product = p,
                                       BrandName = b.BrandName,
                                       CategoryName = c.CategoryName,
                                       SupplierName = ncc.tenNhaCungCap
                                   }).ToList();

            // Thực hiện lọc theo chuỗi không dấu
            var result = filteredProduct.Where(item =>
                RemoveVietnameseDaus(item.Product.ProductName.ToLower()).Contains(normalizedSearchItem) ||
                RemoveVietnameseDaus(item.BrandName.ToLower()).Contains(normalizedSearchItem) ||
                RemoveVietnameseDaus(item.CategoryName.ToLower()).Contains(normalizedSearchItem) ||
                RemoveVietnameseDaus(item.SupplierName.ToLower()).Contains(normalizedSearchItem)
            ).Select(item => item.Product).ToList();

            foreach (var product in result)
            {
                var brand = db.Brands.FirstOrDefault(b => b.BrandID == product.BrandID);
                if (brand != null)
                {
                    product.BrandName = brand.BrandName.ToLower();
                }

                var category = db.Categories.FirstOrDefault(c => c.CategoryID == product.CategoryID);
                if (category != null)
                {
                    product.CategoryName = category.CategoryName.ToLower();
                }

                var supplier = db.NhaCungCaps.FirstOrDefault(n => n.maNhaCungCap == product.maNhaCungCap);
                if (supplier != null)
                {
                    product.SupplierName = supplier.tenNhaCungCap.ToLower();
                }
            }

            return result;
        }

        public string TaoMaSanPham()
        {
            var products = db.Products.ToList();
            if (products.Any())
            {
                var lastProduct = products.OrderByDescending(sp => int.Parse(sp.ProductID.Substring(2))).FirstOrDefault();

                string lastProductID = lastProduct.ProductID;
                int stt = int.Parse(lastProductID.Substring(2)) + 1;

                return "SP" + stt.ToString("D3");
            }
            return "SP001";
        }
    }
}
