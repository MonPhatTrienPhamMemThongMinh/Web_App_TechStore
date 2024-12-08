using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWebGamingGear.Filters;
using DoAnWebGamingGear.Identity;
using DoAnWebGamingGear.Models;

namespace DoAnWebGamingGear.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class UsersController : Controller
    {
        GamingGearDBContext db = new GamingGearDBContext();
        // GET: Admin/Users
        public ActionResult Index()
        {

            List<AppUser> users = db.Users.ToList();
            return View(users);
        }

    }
}