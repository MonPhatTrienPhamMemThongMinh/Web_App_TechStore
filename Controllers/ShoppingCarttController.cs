using DoAnWebGamingGear.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAnWebGamingGear.Controllers
{
    public class ShoppingCarttController : Controller
    {
        GamingGearDBContext db = new GamingGearDBContext();

        // GET: ShoppingCart
        public ActionResult ShowToCart()
        {
            var userId = User.Identity.GetUserId();
            /*if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Điều hướng đến trang đăng nhập nếu chưa đăng nhập
            }
*/
            var cartItems = Session[userId + "_Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cartItems);
        }

        [HttpPost]
        public ActionResult AddToCart(string productid, int quantity = 1)
        {
            var userId = User.Identity.GetUserId();
            var cartItems = Session[userId + "_Cart"] as List<CartItem> ?? new List<CartItem>();

            var product = db.Products.Find(productid);
            if (product != null)
            {
                var existingItem = cartItems.FirstOrDefault(c => c.ProductID == productid);

                if (existingItem != null)
                {
                    existingItem.shopping_quantity += quantity;
                }
                else
                {
                    cartItems.Add(new CartItem
                    {
                        ProductID = productid,
                        Products = product,
                        shopping_quantity = quantity
                    });
                }

                Session[userId + "_Cart"] = cartItems;
            }

            return RedirectToAction("ShowToCart");
        }

        [HttpPost]
        public ActionResult Update_Quantity_Cart(string productid, int quantity = 0)
        {
            var userId = User.Identity.GetUserId();
            var cartItems = Session[userId + "_Cart"] as List<CartItem> ?? new List<CartItem>();

            var item = cartItems.FirstOrDefault(c => c.ProductID == productid);
            if (item != null && quantity > 0)
            {
                item.shopping_quantity = quantity;
                Session[userId + "_Cart"] = cartItems;
            }

            return RedirectToAction("ShowToCart");
        }

        public ActionResult RemoveCart(string productid)
        {
            var userId = User.Identity.GetUserId();
            var cartItems = Session[userId + "_Cart"] as List<CartItem> ?? new List<CartItem>();

            // Kiểm tra xem sản phẩm có trong giỏ hàng không
            var itemToRemove = cartItems.FirstOrDefault(c => c.ProductID == productid);
            if (itemToRemove != null)
            {
                // Xóa sản phẩm khỏi giỏ hàng
                cartItems.Remove(itemToRemove);

                // Cập nhật lại session
                Session[userId + "_Cart"] = cartItems;
            }

            return RedirectToAction("ShowToCart");
        }
        public ActionResult ProceedToPayment()
        {
            var userId = User.Identity.GetUserId();
            var cartItems = Session[userId + "_Cart"] as List<CartItem> ?? new List<CartItem>();

            if (cartItems == null || !cartItems.Any())
            {
                TempData["CartEmptyMessage"] = "Giỏ hàng của bạn đang trống, vui lòng thêm sản phẩm trước khi thanh toán.";
                return RedirectToAction("ShowToCart");
            }

            return RedirectToAction("Index", "Payment");
        }
    }
}
