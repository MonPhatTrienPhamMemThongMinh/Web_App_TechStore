using DoAnWebGamingGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoAnWebGamingGear.ApiControllers
{
    public class CategoriesController : ApiController
    {
        public List<Categories> Get()
        {
            GamingGearDBContext db = new GamingGearDBContext();
            List<Categories> categories = db.Categories.ToList();
            return categories;
        }
    }
}
