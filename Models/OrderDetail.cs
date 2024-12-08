using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        [Required]
        public string ProductID { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public string OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal UnitPrice { get; set; } // đơn giá

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products Products { get; set; }
    }
}
