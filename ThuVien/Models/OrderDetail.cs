using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien.Models
{
    public class OrderDetail
    {
        public string OrderDetailID { get; set; } // Khóa chính
        public string ProductID { get; set; } // Khóa ngoại liên kết với Products
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string OrderId { get; set; } // Khóa ngoại liên kết với Orders
    }
}
