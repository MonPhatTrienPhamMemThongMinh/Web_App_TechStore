using DoAnWebGamingGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DoAnWebGamingGear.ApiControllers
{
    [Route("webhook")]
    public class WebhookController : ApiController
    {
        private GamingGearDBContext db = new GamingGearDBContext();
        [HttpPost]
        public IHttpActionResult HandleWebhook([FromBody] dynamic request)
        {
            string intentName = request.queryResult.intent.displayName;
            string responseText = "";

            if (intentName == "ProductPrice")
            {
                string productName = request.queryResult.parameters.product;
                var product = db.Products.FirstOrDefault(p => p.ProductName == productName);

                if (product != null)
                {
                    responseText = $"Giá của {product.ProductName} là {product.Price} VND.";
                }
                else
                {
                    responseText = "Xin lỗi, tôi không tìm thấy sản phẩm này.";
                }
            }
            else if (intentName == "Category")
            {
                string categoryName = request.queryResult.parameters.category;
                var category = db.Categories.FirstOrDefault(c => c.CategoryName == categoryName);

                if (category != null)
                {
                    var productCount = db.Products.Count(p => p.CategoryID == category.CategoryID);
                    responseText = $"Có {productCount} sản phẩm thuộc loại {categoryName}.";
                }
                else
                {
                    responseText = "Xin lỗi, tôi không tìm thấy loại sản phẩm này.";
                }
            }
            else if (intentName == "CategoryCount")
            {
                var categoryCount = db.Categories.Count();
                responseText = $"Hiện tại cửa hàng đang bán {categoryCount} loại sản phẩm.";
            }
            else if (intentName == "ProductCount")
            {
                var productCount = db.Products.Count();
                responseText = $"Hiện tại cửa hàng đang bán {productCount} sản phẩm.";
            }
            else if (intentName == "BrandCount")
            {
                var brandCount = db.Brands.Count();
                responseText = $"Hiện tại cửa hàng đang có {brandCount} thương hiệu.";
            }

            return Ok(new { fulfillmentText = responseText });
        }
    }
}