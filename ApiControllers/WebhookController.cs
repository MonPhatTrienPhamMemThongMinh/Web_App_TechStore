using DoAnWebGamingGear.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                    responseText = $"Giá của {product.ProductName} là {product.Price.ToString("N0").Replace(",", ".")} VND.";
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
                    var productsInCategory = db.Products.Where(p => p.CategoryID == category.CategoryID).ToList();
                    var productCount = productsInCategory.Count();

                    if (productCount > 0)
                    {
                        // Liệt kê tên các sản phẩm
                        var productNames = string.Join("\n- ", productsInCategory.Select(p => p.ProductName));
                        responseText = $"Có {productCount} sản phẩm thuộc loại {categoryName}:\n- {productNames}";
                    }
                    else
                    {
                        responseText = $"Không có sản phẩm nào thuộc loại {categoryName}.";
                    }
                }
                else
                {
                    responseText = "Xin lỗi, tôi không tìm thấy loại sản phẩm này.";
                }
            }
            else if (intentName == "CategoryCount")
            {
                var categories = db.Categories.ToList();
                var categoryCount = categories.Count();

                if (categoryCount > 0)
                {
                    var categoryNames = string.Join("\n- ", categories.Select(c => c.CategoryName));
                    responseText = $"Hiện tại cửa hàng đang bán {categoryCount} loại sản phẩm:\n- {categoryNames}";
                }
                else
                {
                    responseText = "Hiện tại không có loại sản phẩm nào.";
                }
            }
            else if (intentName == "ProductCount")
            {
                var products = db.Products.ToList();
                var productCount = products.Count();

                if (productCount > 0)
                {
                    var productNames = string.Join("\n- ", products.Select(p => p.ProductName));
                    responseText = $"Hiện tại cửa hàng đang bán {productCount} sản phẩm:\n- {productNames}";
                }
                else
                {
                    responseText = "Hiện tại không có sản phẩm nào.";
                }
            }
            else if (intentName == "BrandCount")
            {
                var brands = db.Brands.ToList();
                var brandCount = brands.Count();

                if (brandCount > 0)
                {
                    var brandNames = string.Join("\n- ", brands.Select(b => b.BrandName));
                    responseText = $"Hiện tại cửa hàng đang có {brandCount} thương hiệu:\n- {brandNames}";
                }
                else
                {
                    responseText = "Hiện tại không có thương hiệu nào.";
                }
            }
            else if (intentName == "HotProducts")
            {
                var hotProducts = db.OrderDetails
                                    .GroupBy(od => od.ProductID)
                                    .Select(g => new
                                    {
                                        ProductID = g.Key,
                                        TotalQuantity = g.Sum(od => od.Quantity)
                                    })
                                    .OrderByDescending(g => g.TotalQuantity)
                                    .Take(5)
                                    .Join(db.Products, g => g.ProductID, p => p.ProductID, (g, p) => p)
                                    .ToList();

                if (hotProducts.Any())
                {
                    responseText = "Các sản phẩm đang hot hiện tại là: " + string.Join(", ", hotProducts.Select(p => p.ProductName));
                }
                else
                {
                    responseText = "Hiện tại không có sản phẩm nào đang hot.";
                }
            }
            else if (intentName == "GoiYSanPhamBanChayTheoLoai")
            {
                string categoryName = request.queryResult.parameters.category; // Lấy tên loại sản phẩm từ Dialogflow
                var category = db.Categories.FirstOrDefault(c => c.CategoryName == categoryName);

                if (category != null)
                {
                    // Lấy danh sách sản phẩm bán chạy trong loại này, tối đa 3 sản phẩm
                    var hotProducts = db.OrderDetails
                                        .Where(od => db.Products
                                                       .Where(p => p.CategoryID == category.CategoryID)
                                                       .Select(p => p.ProductID)
                                                       .Contains(od.ProductID))
                                        .GroupBy(od => od.ProductID)
                                        .Select(g => new
                                        {
                                            ProductID = g.Key,
                                            TotalQuantity = g.Sum(od => od.Quantity)
                                        })
                                        .OrderByDescending(g => g.TotalQuantity)
                                        .Join(db.Products, g => g.ProductID, p => p.ProductID, (g, p) => p)
                                        .Take(3) // Lấy tối đa 3 sản phẩm
                                        .ToList();

                    if (hotProducts.Any())
                    {
                        // Tạo danh sách gợi ý sản phẩmw
                        var suggestions = hotProducts
                            .Select(p => $"{p.ProductName} (Giá: {p.Price} VND)")
                            .ToList();

                        responseText = $"Gợi ý cho bạn các sản phẩm bán chạy nhất trong loại {categoryName}:\n- {string.Join("\n- ", suggestions)}";
                    }
                    else
                    {
                        responseText = $"Hiện tại không có sản phẩm bán chạy nào trong loại {categoryName}.";
                    }
                }
                else
                {
                    responseText = $"Xin lỗi, tôi không tìm thấy loại sản phẩm {categoryName}.";
                }
            }
            else if(intentName == "CheapProduct")
            {
                var cheapestProduct = db.Products
                            .Where(p => p.Price > 0)
                            .OrderBy(p => p.Price)
                            .FirstOrDefault();
                if (cheapestProduct != null)
                {
                    responseText = $"Sản phẩm rẻ nhất hiện tại là {cheapestProduct.ProductName} với giá {cheapestProduct.Price.ToString("N0").Replace(",", ".")}đ.";
                }
                else
                {
                    responseText = "Hiện tại không có sản phẩm nào có giá hợp lệ.";
                }
            }
            else if(intentName == "ExpensiveProduct")
            {
                var mostExpensiveProduct = db.Products
                                 .Where(p => p.Price > 0)
                                 .OrderByDescending(p => p.Price)
                                 .FirstOrDefault();
                if (mostExpensiveProduct != null)
                {
                    responseText = $"Sản phẩm mắc nhất hiện tại là {mostExpensiveProduct.ProductName} với giá {mostExpensiveProduct.Price.ToString("N0").Replace(",", ".")}đ.";
                }
                else
                {
                    responseText = "Hiện tại không có sản phẩm nào có giá hợp lệ.";
                }
            }
            else if(intentName == "ChuongTrinhKhuyenMaiDangDienRa")
            {
                var khuyenMaiDangDienRa = db.KhuyenMais.Where(km => km.trangThai == "Đang diễn ra").ToList();
                if (khuyenMaiDangDienRa.Any())
                {
                    var suggestion = khuyenMaiDangDienRa.Select(km => $"{km.tenKhuyenMai},  thời gian bắt đầu từ {km.ngayBatDau} đến {km.ngayKetThuc}").ToList();
                    responseText = $"Hiện tại đang có chương trình khuyến mãi:\n- {string.Join("\n- ", suggestion)}";
                }
                else
                {
                    responseText = "Hiện tại shop không có chương trình mãi nào cả.";
                }
            }
            else if (intentName == "ProductQuantity")
            {
                string productName = ((JArray)request.queryResult.parameters.product).First.ToString();
                var product = db.Products.FirstOrDefault(p => p.ProductName == productName);

                if (product != null)
                {
                    responseText = $"Sản phẩm {product.ProductName} hiện còn {product.Quantity} sản phẩm trong kho.";
                }
                else
                {
                    responseText = "Xin lỗi, tôi không tìm thấy sản phẩm này.";
                }
            }
            else if (intentName == "CheapestProductByCategory")
            {
                string categoryName = request.queryResult.parameters.category;
                var category = db.Categories.FirstOrDefault(c => c.CategoryName == categoryName);

                if (category != null)
                {
                    var cheapestProduct = db.Products
                                            .Where(p => p.CategoryID == category.CategoryID && p.Price > 0)
                                            .OrderBy(p => p.Price)
                                            .FirstOrDefault();
                    if (cheapestProduct != null)
                    {
                        responseText = $"Sản phẩm rẻ nhất trong loại {categoryName} là {cheapestProduct.ProductName} với giá {cheapestProduct.Price.ToString("N0").Replace(",", ".")}đ.";
                    }
                    else
                    {
                        responseText = $"Hiện tại không có sản phẩm nào có giá hợp lệ trong loại {categoryName}.";
                    }
                }
                else
                {
                    responseText = $"Xin lỗi, tôi không tìm thấy loại sản phẩm {categoryName}.";
                }
            }
            else if (intentName == "MostExpensiveProductByCategory")
            {
                string categoryName = request.queryResult.parameters.category;
                var category = db.Categories.FirstOrDefault(c => c.CategoryName == categoryName);

                if (category != null)
                {
                    var mostExpensiveProduct = db.Products
                                                 .Where(p => p.CategoryID == category.CategoryID && p.Price > 0)
                                                 .OrderByDescending(p => p.Price)
                                                 .FirstOrDefault();
                    if (mostExpensiveProduct != null)
                    {
                        responseText = $"Sản phẩm mắc nhất trong loại {categoryName} là {mostExpensiveProduct.ProductName} với giá {mostExpensiveProduct.Price.ToString("N0").Replace(",", ".")}đ.";
                    }
                    else
                    {
                        responseText = $"Hiện tại không có sản phẩm nào có giá hợp lệ trong loại {categoryName}.";
                    }
                }
                else
                {
                    responseText = $"Xin lỗi, tôi không tìm thấy loại sản phẩm {categoryName}.";
                }
            }

            return Ok(new { fulfillmentText = responseText });
        }
    }
}