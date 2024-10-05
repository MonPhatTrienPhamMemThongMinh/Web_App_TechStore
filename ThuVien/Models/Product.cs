using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien.Models
{
    public class Product
    {
        public string ProductID { get; set; } // Khóa chính
        public string ProductDescription { get; set; }
        public string BrandID { get; set; } // Khóa ngoại liên kết với Brands
        public string CategoryID { get; set; } // Khóa ngoại liên kết với Categories
        public string AvailabilityStatus { get; set; }
        public int Quantity { get; set; }
        public string BaoHanh { get; set; }
        public string ProductName { get; set; }
        public string ProductPic { get; set; }
        public int Price { get; set; }
        public string BrandName { get; set; }
        public string CategoryName
        {
            get; set;
        }
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
}
