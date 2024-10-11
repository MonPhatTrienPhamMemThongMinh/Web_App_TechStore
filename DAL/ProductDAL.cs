using System;
using System.Collections.Generic;
using System.Data;
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


        public Product GetProductById (string productID)
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
                    productToUpdate.Price = product.Price;
                    productToUpdate.Quantity = product.Quantity;
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

        public List<Product> SearchProducts(string searchItem)
        {
            var filteredProduct = from p in db.Products
                                  join b in db.Brands on p.BrandID equals b.BrandID
                                  join c in db.Categories on p.CategoryID equals c.CategoryID
                                  join ncc in db.NhaCungCaps on p.maNhaCungCap equals ncc.maNhaCungCap
                                  where p.ProductName.ToLower().Contains(searchItem.ToLower()) ||
                                        ncc.tenNhaCungCap.ToLower().Contains(searchItem.ToLower()) ||
                                        b.BrandName.ToLower().Contains(searchItem.ToLower()) ||
                                        c.CategoryName.ToLower().Contains(searchItem.ToLower())
                                  select new Product
                                  {
                                      ProductID = p.ProductID,
                                      ProductName = p.ProductName,
                                      ProductDescription = p.ProductDescription,
                                      BrandID = p.BrandID,
                                      CategoryID = p.CategoryID,
                                      AvailabilityStatus = p.AvailabilityStatus,
                                      Quantity = p.Quantity,
                                      BaoHanh = p.BaoHanh,
                                      ProductPic = p.ProductPic,
                                      Price = p.Price,
                                      BrandName = b.BrandName,
                                      CategoryName = c.CategoryName,
                                      maNhaCungCap = ncc.maNhaCungCap,
                                      SupplierName = ncc.tenNhaCungCap
                                  };

            return filteredProduct.ToList();
        }
    }
}
