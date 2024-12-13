using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class KhuyenMaiSanPham
    {
        [StringLength(50)]
        public string maKhuyenMai { get; set; }
        [StringLength(50)]
        public string maSanPham { get; set; }        
        public decimal phanTramGiam { get; set; }

        [StringLength(50)]
        public string trangThai { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
        public virtual Products Products { get; set; }
    }
}