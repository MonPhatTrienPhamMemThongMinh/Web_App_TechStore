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
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private readonly GamingGearDBContext db;

        public CategoriesController()
        {
            db = new GamingGearDBContext();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCategories()
        {
            var categories = db.Categories
                .Select(c => new Category
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    CategoryDescription = c.CategoryDescription,
                    CategoryPic = c.CategoryPic,
                    Published = c.Published,
                    CategoryBackground = c.CategoryBackground,
                    CategoryAvatar = c.CategoryAvatar
                })
                .ToList();

            return Ok(categories);
        }
    }
}
