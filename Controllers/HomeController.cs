using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWebGamingGear.Models;

namespace DoAnWebGamingGear.Controllers
{
    public class HomeController : Controller
    {
        GamingGearDBContext db = new GamingGearDBContext();
        // GET: Home
        public ActionResult Index(string search = "")
        {
            List<Products> products = db.Products.Where(row => row.ProductName.Contains(search)).ToList();
            ViewBag.Search = search;

            // Lấy danh sách các thương hiệu
            var brands = db.Brands.ToList();
            ViewBag.Brands = brands;

            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;

            return View(products);
        }
        // Mat hang

        // Thuong hieu
        public ActionResult BrandDetail(string id)
        {
            var brand = db.Brands.Include("Products").FirstOrDefault(b => b.BrandID == id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
    }
}