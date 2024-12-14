using DoAnWebGamingGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;

namespace DoAnWebGamingGear.ApiControllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly GamingGearDBContext db;

        public ProductsController()
        {
            db = new GamingGearDBContext();
        }

        [HttpGet]
        [Route("price")]
        public IHttpActionResult GetProductPrice(string productName)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductName == productName);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new { Price = product.Price });
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProducts()
        {
            var products = db.Products
                .Select(p => new Product
                {
                    ProductID = p.ProductID,
                    ProductDescription = p.ProductDescription,
                    BrandID = p.BrandID,
                    CategoryID = p.CategoryID,
                    AvailabilityStatus = p.AvailabilityStatus,
                    Quantity = p.Quantity,
                    BaoHanh = p.BaoHanh,
                    ProductName = p.ProductName,
                    ProductPic = p.ProductPic,
                    Price = p.Price
                })
                .ToList();

            return Ok(products);
        }
    }
}
