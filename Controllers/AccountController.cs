using DoAnWebGamingGear.Identity;
using DoAnWebGamingGear.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DoAnWebGamingGear.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoAnWebGamingGear.Controllers
{
    public class AccountController : Controller
    {
        GamingGearDBContext db = new GamingGearDBContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM rmv)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new GamingGearDBContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);

                var existingUser = userManager.FindByName(rmv.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UsernameExists", "Tên tài khoản đã tồn tại");
                    return View(rmv);
                }

                var passwdHash = Crypto.HashPassword(rmv.Password);
                var user = new AppUser()
                {
                    Email = rmv.Email,
                    UserName = rmv.Username,
                    PasswordHash = passwdHash,
                    FullName = rmv.FullName,
                    City = rmv.City,
                    Birthday = rmv.DateOfBirth,
                    Address = rmv.Address,
                    PhoneNumber = rmv.Phone
                };
                IdentityResult identityResult = userManager.Create(user); // cho biết kết quả
                if (identityResult.Succeeded)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(appDbContext));
                    if (!roleManager.RoleExists("Customer"))
                    {
                        roleManager.Create(new IdentityRole("Customer"));
                    }
                    userManager.AddToRole(user.Id, "Customer");

                    var authenManager = HttpContext.GetOwinContext().Authentication; // cho user login
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    };
                }
            }
            else
            {
                ModelState.AddModelError("Lỗi", "Dữ liêu không hợp lệ");
            }
            return View(rmv);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM lvm)
        {
            if (string.IsNullOrEmpty(lvm.Username) || string.IsNullOrEmpty(lvm.Password))
            {
                //ModelState.AddModelError("Error", "Vui lòng nhập tài khoản và mật khẩu");
                return View(lvm);
            }
            else
            {
                var appDbContext = new GamingGearDBContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var user = userManager.Find(lvm.Username, lvm.Password);
                if (user != null)
                {
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                    if (userManager.IsInRole(user.Id, "Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Sai tài khoản hoặc mật khẩu");
                    return View();
                }

            }

        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyProfile()
        {
            var appDbContext = new GamingGearDBContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            var profileVM = new MyProfile
            {
                Username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                DateOfBirth = user.Birthday,
                City = user.City,
                Address = user.Address,
                FullName = user.FullName,
                Orders = user.Orders
            };
            return View(profileVM);
        }

        public ActionResult OrderHistory()
        {
            var userId = User.Identity.GetUserId();
            var orders = db.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.CreatedDate).ToList();
            return View(orders);
        }

        public ActionResult OrderDetails(string id)
        {
            var order = db.Orders.Include("OrderDetails").FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}