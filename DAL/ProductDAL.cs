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
        public int TongSoLuongSanPham()
        {
            return db.Products.Count();
        }
        public List<(string TenSanPham, int SoLuong)> ThongKeDanhSachSanPhamDuoiMucToiThieu()
        {
            var sanPhamDuoiMucToiThieu = db.Products
                                .Where(sp => sp.Quantity < 40)
                                .Select(sp => new
                                {
                                    TenSanPham = sp.ProductName,
                                    SoLuong = sp.Quantity
                                })
                                .ToList();
            return sanPhamDuoiMucToiThieu.Select(sp => (sp.TenSanPham, sp.SoLuong)).ToList();
        }
        public List<SanPhamBanChay> ThongKeTop5SanPhamBanChayNhat(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            var topSanPham = db.OrderDetails
                                .Where(ct => ct.Order.CreatedDate.Date >= ngayBatDau.Date && ct.Order.CreatedDate.Date <= ngayKetThuc.Date)
                                .GroupBy(ct => ct.ProductID)
                                .Select(tk => new
                                {
                                    MaSanPham = tk.Key,
                                    TongSoLuong = tk.Sum(ct => ct.Quantity)
                                })
                                .OrderByDescending(tk => tk.TongSoLuong)
                                .Take(5)
                                .Join(db.Products, tk => tk.MaSanPham, sp => sp.ProductID, (tk, sp) => new SanPhamBanChay
                                {
                                    MaSanPham = sp.ProductID,
                                    TenSanPham = sp.ProductName,
                                    SoLuongBan = tk.TongSoLuong
                                })
                                .ToList();

            if (!topSanPham.Any())
            {
                return new List<SanPhamBanChay>();
            }
            return topSanPham;
        }
        public List<Product> GetAllProducts()
        {
            var products = (from p in db.Products
                            join b in db.Brands on p.BrandID equals b.BrandID
                            join c in db.Categories on p.CategoryID equals c.CategoryID
                            select p).Where(p=>p.AvailabilityStatus!="OutOfStock").ToList();
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
        public bool DeleteProduct(string maSanPham)
        {
            try
            {
                Product productToDelete = db.Products.FirstOrDefault(p => p.ProductID == maSanPham);

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
                                   select new
                                   {
                                       Product = p,
                                       BrandName = b.BrandName,
                                       CategoryName = c.CategoryName
                                   }).ToList();

            // Thực hiện lọc theo chuỗi không dấu
            var result = filteredProduct.Where(item =>
                RemoveVietnameseDaus(item.Product.ProductName.ToLower()).Contains(normalizedSearchItem) ||
                RemoveVietnameseDaus(item.BrandName.ToLower()).Contains(normalizedSearchItem) ||
                RemoveVietnameseDaus(item.CategoryName.ToLower()).Contains(normalizedSearchItem)
            ).Select(item => item.Product).ToList();

            foreach (var product in result)
            {
                var brand = db.Brands.FirstOrDefault(b => b.BrandID == product.BrandID);
                if (brand != null)
                {
                    product.BrandName = brand.BrandName;
                }

                var category = db.Categories.FirstOrDefault(c => c.CategoryID == product.CategoryID);
                if (category != null)
                {
                    product.CategoryName = category.CategoryName;
                }
            }

            return result;
        }
        public List<Product> LocSanPhamTheoLoai(string maLoaiSP)
        {
            List<Product> danhSach = db.Products.Where(p=>p.CategoryID == maLoaiSP).ToList();
            foreach(var product in danhSach)
            {
                var brand = db.Brands.FirstOrDefault(b => b.BrandID == product.BrandID);
                if (brand != null)
                {
                    product.BrandName = brand.BrandName;
                }

                var category = db.Categories.FirstOrDefault(c => c.CategoryID == product.CategoryID);
                if (category != null)
                {
                    product.CategoryName = category.CategoryName;
                }
            }
            return danhSach;
        }
        public List<Product> LocSanPhamTheoThuongHieu(string maThuongHieu)
        {
            List<Product> danhSach = db.Products.Where(p => p.BrandID == maThuongHieu).ToList();
            foreach (var product in danhSach)
            {
                var brand = db.Brands.FirstOrDefault(b => b.BrandID == product.BrandID);
                if (brand != null)
                {
                    product.BrandName = brand.BrandName;
                }

                var category = db.Categories.FirstOrDefault(c => c.CategoryID == product.CategoryID);
                if (category != null)
                {
                    product.CategoryName = category.CategoryName;
                }
            }
            return danhSach;
        }
        public List<Product> LocSanPhamTheoTrangThai(string maTrangThai)
        {
            List<Product> danhSach = db.Products.Where(p => p.AvailabilityStatus == maTrangThai).ToList();
            foreach (var product in danhSach)
            {
                var brand = db.Brands.FirstOrDefault(b => b.BrandID == product.BrandID);
                if (brand != null)
                {
                    product.BrandName = brand.BrandName;
                }

                var category = db.Categories.FirstOrDefault(c => c.CategoryID == product.CategoryID);
                if (category != null)
                {
                    product.CategoryName = category.CategoryName;
                }
            }
            return danhSach;
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
        public bool KiemTraSanPhamCoThuocHoaDon(string maSanPham)
        {
            int dem = db.OrderDetails.Where(sp => sp.ProductID == maSanPham).Count();
            if (dem > 0)
            {
                return true;
            }
            return false;
        }
        public bool KiemTraSanPhamCoThuocPhieuDat(string maSanPham)
        {
            int dem = db.ChiTietPhieuDats.Where(sp => sp.ProductID == maSanPham).Count();
            if (dem > 0)
            {
                return true;
            }
            return false;
        }
    }
}
