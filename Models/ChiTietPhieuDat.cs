using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class ChiTietPhieuDat
    {
        public string MaPhieuDat { get; set; }
        public string ProductID { get; set; }

        public int SoLuongDat { get; set; }
        public int SoLuongNhan { get; set; }
        public decimal DonGia { get; set; }
        public decimal TongTien { get; set; }

        [ForeignKey("MaPhieuDat")]
        public virtual PhieuDat PhieuDat { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products Products { get; set; }

        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}