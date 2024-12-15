using DoAnWebGamingGear.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWebGamingGear.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; }

        [Required]
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        [Required]
        public string CustomerName { get; set; }

        [Required, Phone]
        public string CustomerPhone { get; set; }

        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerProvince { get; set; }
        [Required]
        public string CustomerDistrict { get; set; }
        [Required]
        public string CustomerWard { get; set; }

        [Required, EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [ForeignKey("UserID")]
        public virtual AppUser AppUser { get; set; }
    }
}