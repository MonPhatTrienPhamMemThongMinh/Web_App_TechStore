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
    [RoutePrefix("api/brands")]
    public class BrandsController : ApiController
    {
        private readonly GamingGearDBContext db;

        public BrandsController()
        {
            db = new GamingGearDBContext();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBrands()
        {
            var brands = db.Brands
                .Select(b => new Brand
                {
                    BrandID = b.BrandID,
                    BrandName = b.BrandName,
                    BrandDescription = b.BrandDescription,
                    BrandPic = b.BrandPic,
                    BrandBackground = b.BrandBackground
                })
                .ToList();

            return Ok(brands);
        }
    }
}
