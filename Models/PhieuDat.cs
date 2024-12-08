using DoAnWebGamingGear.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class PhieuDat
    {
        [Key]
        public string MaPhieuDat { get; set; }

        [Required]
        public string UserID { get; set; }

        public string MaNhaCungCap { get; set; }
        public DateTime NgayLap { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public string TrangThaiXacNhan { get; set; }
        public string GhiChu { get; set; }

        [ForeignKey("MaNhaCungCap")]
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<ChiTietPhieuDat> ChiTietPhieuDats { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}