using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        public int shopping_quantity { get; set; }
        public string ProductID { get; set; }
        public int AvailableQuantity { get; set; }
        public virtual Products Products { get; set; }
    }
}