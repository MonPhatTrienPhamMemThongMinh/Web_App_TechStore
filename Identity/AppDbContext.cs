using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWebGamingGear.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoAnWebGamingGear.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("MyCS")
        { }
    }
}