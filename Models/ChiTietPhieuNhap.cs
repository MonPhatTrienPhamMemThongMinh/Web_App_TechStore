using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class ChiTietPhieuNhap
    {
        [Key, Column(Order = 0)]
        [Required]
        [ForeignKey("PhieuNhap")]
        public string maPhieuNhap { get; set; }
        public virtual PhieuNhap PhieuNhap { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("Products")]
        public string ProductID { get; set; }
        public virtual Products Products { get; set; }

    }
}