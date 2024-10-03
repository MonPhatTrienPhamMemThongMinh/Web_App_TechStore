using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien.Models
{
    public class Order
    {
        public string OrderId { get; set; } // Khóa chính
        public string OrderName { get; set; }
        public string UserId { get; set; } // Khóa ngoại liên kết với AspNetUsers
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
