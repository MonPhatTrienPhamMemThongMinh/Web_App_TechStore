using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien.Models
{
    public class Supplier
    {
        public string SupplierID { get; set; } // Khóa chính
        public string SupplierName { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierAddress { get; set; }
    }
}
