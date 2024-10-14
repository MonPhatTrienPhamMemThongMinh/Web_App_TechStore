using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAL cdal = new CategoryDAL();
        public CategoryBLL() { }

        public List<Category> GetAllCategories()
        {
            return cdal.GetAllCategories();
        }

        public Category GetCategoryById(string categoryId)
        {
            try
            {
                return cdal.GetCategoryById(categoryId);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Lỗi khi lấy thông tin mặt hàng", ex);
            }
        }

        public bool DeleteCategory(Category category)
        {
            return cdal.DeleteCategory(category);
        }

        public bool AddCategory(Category category)
        {
            return cdal.AddCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            return cdal.UpdateCategory(category);
        }

        public string GenerateCategoryID()
        {
            return cdal.GenerateCategoryID();
        }
    }
}
