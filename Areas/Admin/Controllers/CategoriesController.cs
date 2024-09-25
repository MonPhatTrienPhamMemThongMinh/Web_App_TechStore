using DoAnWebGamingGear.Filters;
using DoAnWebGamingGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebGamingGear.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class CategoriesController : Controller
    {
        // GET: Admin/Categories
        GamingGearDBContext db = new GamingGearDBContext();
        // GET: Categories
        public ActionResult Index()
        {
            List<Categories> categories = db.Categories.ToList();
            return View(categories);
        }
        public ActionResult DetailKeyBoardCategory(string id = "MH001")
        {
            Categories banphimco = db.Categories.Where(row => row.CategoryID.Contains(id)).FirstOrDefault();
            return View(banphimco);
        }
        public ActionResult DetailMouseCategory(string id = "MH002")
        {
            Categories chuot = db.Categories.Where(row => row.CategoryID.Contains(id)).FirstOrDefault();
            return View(chuot);
        }
        public ActionResult DetailMousePadCategory(string id = "MH003")
        {
            Categories lotchuot = db.Categories.Where(row => row.CategoryID.Contains(id)).FirstOrDefault();
            return View(lotchuot);
        }
        public ActionResult DetailHeadPhoneCategory(string id = "MH004")
        {
            Categories tainghe = db.Categories.Where(row => row.CategoryID.Contains(id)).FirstOrDefault();
            return View(tainghe);
        }
        public ActionResult DetailSwitchLubeCategory(string id = "MH005")
        {
            Categories switchlube = db.Categories.Where(row => row.CategoryID.Contains(id)).FirstOrDefault();
            return View(switchlube);
        }
    }
}