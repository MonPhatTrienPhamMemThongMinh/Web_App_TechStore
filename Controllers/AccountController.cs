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

namespace DoAnWebGamingGear.Controllers
{
    public class AccountController : Controller
    {
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
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var passwdHash = Crypto.HashPassword(rmv.Password);
                var user = new AppUser() // Biến chứa thông tin user
                {
                    Email = rmv.Email,
                    UserName = rmv.Username,
                    PasswordHash = passwdHash,
                    City = rmv.City,
                    Birthday = rmv.DateOfBirth,
                    Address = rmv.Address,
                    PhoneNumber = rmv.Phone
                };
                IdentityResult identityResult = userManager.Create(user); // cho biết kết quả
                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer"); //thiết lập role

                    var authenManager = HttpContext.GetOwinContext().Authentication; // cho user login
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Lỗi", "Dữ liêu không hợp lệ");
                return View();
            }
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
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var user = userManager.Find(lvm.Username, lvm.Password);
                if (user != null)
                {
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                    if(userManager.IsInRole(user.Id, "Admin"))
                    {
                        return RedirectToAction("Index", "Home", new {area = "Admin"});
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
            var appDbContext = new AppDbContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            var registerVM = new RegisterVM
            {
                Username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                City = user.City,
                DateOfBirth = user.Birthday,
                Address = user.Address
            };
            return View(registerVM);
        }
    }
}