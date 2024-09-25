using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoAnWebGamingGear.Models
{
    public class Brands
    {
        [Key]
        public string BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandPic { get; set; }
        public string BrandBackground { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}