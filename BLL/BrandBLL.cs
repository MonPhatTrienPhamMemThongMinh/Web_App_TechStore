using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BrandBLL
    {
        BrandDAL bdal = new BrandDAL();
        public BrandBLL() { }

        public List<Brand> GetAllBrands()
        {
            return bdal.GetAllBrands();
        }
        public int DemSoSanPhamThuocThuongHieu(string maThuongHieu)
        {
            return bdal.DemSoSanPhamThuocThuongHieu(maThuongHieu);
        }

        public Brand GetBrandById(string brandId)
        {
            try
            {
                return bdal.GetBrandById(brandId);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Lỗi khi lấy thông tin hãng sản phẩm", ex);
            }
        }

        public bool DeleteBrand(Brand brand)
        {
            return bdal.DeleteBrand(brand);
        }

        public bool AddBrand(Brand brand)
        {
            return bdal.AddBrand(brand);
        }

        public bool UpdateBrand(Brand brand)
        {
            return bdal.UpdateBrand(brand);
        }

        public string GenerateBrandID()
        {
            return bdal.GenerateBrandID();
        }
    }
}
