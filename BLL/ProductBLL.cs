using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class ProductBLL
    {
        ProductDAL pdal = new ProductDAL();
        public ProductBLL() { }

        public List<Product> GetAllProducts()
        {
            return pdal.GetAllProducts();
        }

        public Product GetProductById(string productId)
        {
            try
            {
                return pdal.GetProductById(productId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi khi lấy thông tin sản phẩm", ex);
            }
        }

        public List<Product> SearchProducts(string searchItem)
        {
            return pdal.SearchProducts(searchItem);
        }

        public bool AddProduct(Product product)
        {
            return pdal.AddProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            return pdal.UpdateProduct(product);
        }
        public bool DeleteProduct(Product product)
        {
            return pdal.DeleteProduct(product);
        }

        public string TaoMaSanPham()
        {
            return pdal.TaoMaSanPham();
        }
    }
}
