using DoAnWebGamingGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoAnWebGamingGear.ApiControllers
{
    public class ProductsController : ApiController
    {
        public List<Products> Get()
        {
            GamingGearDBContext db = new GamingGearDBContext();
            List<Products> products = db.Products.ToList();
            return products;
        }
    }
}
