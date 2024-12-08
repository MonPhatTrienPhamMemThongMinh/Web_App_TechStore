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
    public class BrandDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();

        public BrandDAL()
        {
        }

        public List<Brand> GetAllBrands()
        {
            return db.Brands.Select(b => b).ToList<Brand>();
        }

        public Brand GetBrandById(string brandId)
        {
            try
            {
                Brand brand = db.Brands.FirstOrDefault(b => b.BrandID == brandId);
                return brand;
            }
            catch
            {
                return null;
            }
        }

        public bool AddBrand(Brand brand)
        {
            try
            {
                db.Brands.InsertOnSubmit(brand);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateBrand(Brand brand)
        {
            try
            {
                Brand newBrand = db.Brands.FirstOrDefault(b => b.BrandID == brand.BrandID);

                if (newBrand != null)
                {
                    newBrand.BrandName = brand.BrandName;
                    newBrand.BrandPic = brand.BrandPic;
                    newBrand.BrandBackground = brand.BrandBackground;
                    newBrand.BrandDescription = brand.BrandDescription;

                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi sửa hãng sản phẩm: " + ex.Message, ex);
            }
        }

        public string GenerateBrandID()
        {
            var brands = db.Brands.ToList();
            if (brands.Any())
            {
                var lastBrand = brands.OrderByDescending(br => int.Parse(br.BrandID.Substring(2))).FirstOrDefault();

                string lastBrandID = lastBrand.BrandID;
                int stt = int.Parse(lastBrandID.Substring(2)) + 1;

                return "BR" + stt.ToString("D3");
            }
            return "BR001";
        }

        public bool DeleteBrand(Brand brand)
        {
            try
            {
                var products = db.Products.Where(p => p.BrandID == brand.BrandID).ToList();
                db.Products.DeleteAllOnSubmit(products);

                Brand brandToDelete = db.Brands.FirstOrDefault(b => b.BrandID == brand.BrandID);
                if (brandToDelete != null)
                {
                    db.Brands.DeleteOnSubmit(brandToDelete);
                }
                db.SubmitChanges();
                return true;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Lỗi xóa hãng sản phẩm: " + ex.Message, ex);
            }
        }
    }
}
