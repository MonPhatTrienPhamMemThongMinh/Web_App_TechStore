using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien.Models
{
    public class Brand
    {
        public string BrandID { get; set; } // Khóa chính
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandPic { get; set; }
        public string BrandBackground { get; set; }
    }
}
