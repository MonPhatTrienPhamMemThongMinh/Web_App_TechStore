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
        public string MaPhieuNhap { get; set; }
        public string ProductID { get; set; }
        public string MaPhieuDat { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgaySanXuat { get; set; }
        public DateTime HanSuDung { get; set; }
        public decimal TongTien { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }
        public virtual ChiTietPhieuDat ChiTietPhieuDat { get; set; }

    }
}