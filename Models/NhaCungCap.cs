using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class NhaCungCap
    {
        [Key]
        [StringLength(10)]
        public string maNhaCungCap { get; set; }

        [Required]
        [StringLength(100)]
        public string tenNhaCungCap { get; set; }

        [StringLength(15)]
        public string soDienThoai { get; set; }

        [StringLength(255)]
        public string diaChi { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}