using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class PhieuNhap
    {
        [Key]
        public string maPhieuNhap { get; set; }
        [Required]
        public string maNhanVien { get; set; }
        [Required]
        [ForeignKey("NhaCungCap")]
        public string maNhaCungCap { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }

        public DateTime ngayLap { get; set; }
        public decimal? tongTien { get; set; }
    }
}