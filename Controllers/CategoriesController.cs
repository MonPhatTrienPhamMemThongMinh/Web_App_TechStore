using DoAnWebGamingGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebGamingGear.Controllers
{
    public class CategoriesController : Controller
    {
        GamingGearDBContext db = new GamingGearDBContext();

        public ActionResult Index()
        {
            List<Categories> categories = db.Categories.ToList();
            return View(categories);
        }
        public ActionResult Detail(string id)
        {
            var category = db.Categories
                            .Include("Products")
                            .FirstOrDefault(c => c.CategoryID == id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
    }
}