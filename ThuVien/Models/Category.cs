using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien.Models
{
    public class Category
    {
        public string CategoryID { get; set; } // Khóa chính
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryPic { get; set; }
        public int? Published { get; set; }
        public string CategoryBackground { get; set; }
        public string CategoryAvatar { get; set; }
    }
}
