using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DoAnWebGamingGear.Models;

namespace DoAnWebGamingGear.Controllers
{
    public class ProductsController : Controller
    {
        private readonly GamingGearDBContext db = new GamingGearDBContext();

        public ActionResult Index(string search = "", string Sort = "ProductID", string[] selectedCategories = null, string Icon = "fa-sort-asc", int page = 1, string[] selectedBrands = null, bool resetPage = false)
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();

            // Nếu không có category hoặc brand nào được chọn, khởi tạo mảng trống
            selectedCategories = selectedCategories?.SelectMany(s => s.Split(',')).ToArray() ?? new string[0];
            selectedBrands = selectedBrands?.SelectMany(s => s.Split(',')).ToArray() ?? new string[0];

            ViewBag.SelectedCategories = selectedCategories.ToList();
            ViewBag.SelectedBrands = selectedBrands.ToList();
            ViewBag.Search = search;
            ViewBag.Sort = Sort;
            ViewBag.Icon = Icon;

            if (resetPage)
            {
                page = 1;
            }

            // Khởi tạo query sản phẩm
            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string searchKhongDau = StringHelper.RemoveVietnameseDaus(search.ToLower().Trim());
                products = products.ToList().Where(p =>
                    StringHelper.RemoveVietnameseDaus(p.ProductName.ToLower()).Contains(searchKhongDau)
                ).AsQueryable();
            }

            // Lọc sản phẩm theo danh mục và thương hiệu nếu có
            if (selectedCategories.Any())
            {
                products = products.Where(p => selectedCategories.Contains(p.CategoryID.ToString()));
            }

            if (selectedBrands.Any())
            {
                products = products.Where(p => selectedBrands.Contains(p.BrandID.ToString()));
            }

            ViewBag.SortC = Sort;
            ViewBag.IconC = Icon;
            switch (Sort)
            {
                case "ProductName":
                    products = Icon == "fa-sort-asc" ? products.OrderBy(row => row.ProductName) : products.OrderByDescending(row => row.ProductName);
                    break;
                case "Price":
                    products = Icon == "fa-sort-asc" ? products.OrderBy(row => row.Price) : products.OrderByDescending(row => row.Price);
                    break;
                default:
                    products = Icon == "fa-sort-asc" ? products.OrderBy(row => row.ProductID) : products.OrderByDescending(row => row.ProductID);
                    break;
            }

            int NoOfRecordPerPage = 6;
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;

            int totalProducts = products.Count();
            int NoOfPages = (int)Math.Ceiling((double)totalProducts / NoOfRecordPerPage);
            ViewBag.NoOfPages = NoOfPages;

            // Lấy dữ liệu đã lọc và phân trang
            return View(products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList());
        }

        public ActionResult Detail(string id)
        {
            var pro = db.Products.FirstOrDefault(row => row.ProductID == id);
            return View(pro);
        }
    }
}
