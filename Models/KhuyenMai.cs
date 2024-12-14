using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class KhuyenMai
    {
        [Key]
        [StringLength(50)]
        public string maKhuyenMai { get; set; }

        [Required]
        [StringLength(100)]
        public string tenKhuyenMai { get; set; }

        [StringLength(255)]
        public string moTa { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }

        public DateTime ngayBatDau { get; set; }
        public DateTime ngayKetThuc { get; set; }
        public virtual ICollection<KhuyenMaiSanPham> KhuyenMaiSanPhams { get; set; }
    }
}