using DoAnWebGamingGear.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebGamingGear.Controllers
{
    public class DialogflowController : Controller
    {
        private GamingGearDBContext db = new GamingGearDBContext();

        [HttpPost]
        public ActionResult Post()
        {
            // Đọc dữ liệu từ request body
            var request = JObject.Parse(new System.IO.StreamReader(Request.InputStream).ReadToEnd());
            var queryResult = request["queryResult"];
            var parameters = queryResult["parameters"];
            var productName = parameters["ProductName"].ToString();

            // Lấy giá sản phẩm từ cơ sở dữ liệu
            string price = GetProductPrice(productName);

            // Tạo response cho Dialogflow
            var response = new
            {
                fulfillmentText = $"Giá của {productName} là {price} VND."
            };

            return Json(response);
        }

        private string GetProductPrice(string productName)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductName == productName);
            return product != null ? product.Price.ToString() : "không tìm thấy sản phẩm";
        }
    }
}