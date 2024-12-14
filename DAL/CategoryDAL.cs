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
    public class CategoryDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public CategoryDAL()
        {

        }

        public List<Category> GetAllCategories()
        {
            return db.Categories.Select(c => c).ToList();
        }

        public Category GetCategoryById(string categoryId)
        {
            try
            {
                Category category = db.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
                return category;
            }
            catch
            {
                return null;
            }
        }

        public bool AddCategory(Category category)
        {
            try
            {
                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCategory(Category category)
        {
            try
            {
                Category newCategory = db.Categories.FirstOrDefault(c => c.CategoryID == category.CategoryID);

                if (newCategory != null)
                {
                    newCategory.CategoryName = category.CategoryName;
                    newCategory.CategoryPic = category.CategoryPic;
                    newCategory.CategoryAvatar = category.CategoryAvatar;
                    newCategory.CategoryDescription = category.CategoryDescription;

                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi sửa thông tin mặt hàng: " + ex.Message, ex);
            }
        }

        public string GenerateCategoryID()
        {
            var categories = db.Categories.ToList();
            if (categories.Any())
            {
                var lastCategory = categories.OrderByDescending(ct => int.Parse(ct.CategoryID.Substring(2))).FirstOrDefault();

                string lastCategoryID = lastCategory.CategoryID;
                int stt = int.Parse(lastCategoryID.Substring(2)) + 1;

                return "MH" + stt.ToString("D3");
            }
            return "MH001";
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                var products = db.Products.Where(p => p.CategoryID == category.CategoryID).ToList();
                db.Products.DeleteAllOnSubmit(products);

                Category categoryToDelete = db.Categories.FirstOrDefault(c => c.CategoryID == category.CategoryID);
                if (categoryToDelete != null)
                {
                    db.Categories.DeleteOnSubmit(categoryToDelete);
                }
                db.SubmitChanges();
                return true;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Lỗi xóa mặt hàng: " + ex.Message, ex);
            }
        }
        public int DemSoSanPhamThuocLoai(string maLoaiSanPham)
        {
            return db.Categories.Where(sp => sp.CategoryID == maLoaiSanPham).Count();
        }
    }
}
