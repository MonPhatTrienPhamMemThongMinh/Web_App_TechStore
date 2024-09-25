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
        [Key]
        public string OrderDetailID { get; set; }
        [Required]
        public string ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductID")]
        public virtual Products Products { get; set; }
    }
}