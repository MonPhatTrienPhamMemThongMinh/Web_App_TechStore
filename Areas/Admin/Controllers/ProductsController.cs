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
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        GamingGearDBContext db = new GamingGearDBContext();
        // GET: Products
        public ActionResult Index(string search = "")
        {
            List<Products> products = db.Products.Where(row => row.ProductName.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(products);
        }
        public ActionResult Detail(string id)
        {
            Products pro = db.Products.Where(row => row.ProductID.Contains(id)).FirstOrDefault();
            return View(pro);
        }
        public ActionResult Edit(string id)
        {
            Products products = db.Products.Where(row => row.ProductID.Contains(id)).FirstOrDefault();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View(products);
        }
        [HttpPost]
        public ActionResult Edit(Products products, HttpPostedFileBase ProductPic)
        {
            Products pro = db.Products.Where(row => row.ProductID.Contains(products.ProductID)).FirstOrDefault();

            //Update
            if (ProductPic != null)
            {
                var UrlTuongDoi = "/Pic/";
                var UrlTuyetDoi = Server.MapPath(UrlTuongDoi);
                ProductPic.SaveAs(UrlTuyetDoi + ProductPic.FileName);
                pro.ProductPic = UrlTuongDoi + ProductPic.FileName;
            }
            pro.ProductName = products.ProductName;
            pro.ProductDescription = products.ProductDescription;
            pro.Price = products.Price;
            pro.BrandID = products.BrandID;
            pro.CategoryID = products.CategoryID;
            pro.Quantity = products.Quantity;
            pro.BaoHanh = products.BaoHanh;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            Products products = db.Products.Where(row => row.ProductID.Contains(id)).FirstOrDefault();
            return View(products);
        }
        [HttpPost]
        public ActionResult Delete(string id, Products pro)
        {
            Products products = db.Products.Where(row => row.ProductID.Contains(id)).FirstOrDefault();
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Products product, HttpPostedFileBase ProductPic)
        {
            if(ModelState.IsValid)
            {
                if (ProductPic != null)
                {
                    var UrlTuongDoi = "/Pic/";
                    var UrlTuyetDoi = Server.MapPath(UrlTuongDoi);
                    ProductPic.SaveAs(UrlTuyetDoi + ProductPic.FileName);
                    product.ProductPic = UrlTuongDoi + ProductPic.FileName;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Create", "Products", new { area = "Admin"});
            }    
        }

    }
}