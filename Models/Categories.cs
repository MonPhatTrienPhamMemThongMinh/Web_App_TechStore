using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.Models
{
    public class Categories
    {
        [Key]
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryPic { get; set; }
        public Nullable<int> Published { get; set; }
        public string CategoryBackground { get; set; }
        public string CategoryAvatar { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}