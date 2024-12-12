using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DoAnWebGamingGear.Identity;

namespace DoAnWebGamingGear.Models
{
    public class PhieuNhap
    {
        [Key]
        public string MaPhieuNhap { get; set; }

        public string MaPhieuDat { get; set; }
        public DateTime NgayNhap { get; set; }
        public int SoLan { get; set; }
        public decimal TongTien { get; set; }

        [ForeignKey("MaPhieuDat")]
        public virtual PhieuDat PhieuDat { get; set; }

        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}