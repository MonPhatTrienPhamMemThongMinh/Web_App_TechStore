using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWebGamingGear.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoAnWebGamingGear.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ICollection<Order> Orders { get; internal set; }
    }
}