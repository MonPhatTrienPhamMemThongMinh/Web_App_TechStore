using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWebGamingGear.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoAnWebGamingGear.Identity
{
    public class AppUserStore : UserStore<AppUser>
    {
        public AppUserStore(GamingGearDBContext dbContext) : base(dbContext) { }
    }
}