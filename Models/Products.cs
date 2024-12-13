using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWebGamingGear.Models
{
    public class Products
    {
        [Key]
        public string ProductID { get; set; }
        public string ProductDescription { get; set; }

        [Required]
        public string BrandID { get; set; }

        [Required]
        public string CategoryID { get; set; }
        public string AvailabilityStatus { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string BaoHanh { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductPic { get; set; }

        [Required]
        public int Price { get; set; }
        public int SalePrice { get;set; }
        public virtual Brands Brands { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<KhuyenMaiSanPham> KhuyenMaiSanPhams { get; set; }
    }
}