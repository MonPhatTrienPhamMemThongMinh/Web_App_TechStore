namespace DoAnWebGamingGear.Migrations
{
    using DoAnWebGamingGear.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DoAnWebGamingGear.Models.GamingGearDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DoAnWebGamingGear.Models.GamingGearDBContext";
        }

        protected override void Seed(DoAnWebGamingGear.Models.GamingGearDBContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new AppUserManager(new AppUserStore(context));

            // Tạo vai trò Admin nếu chưa có
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);
            }

            // Tạo tài khoản admin nếu chưa có
            if (userManager.FindByName("admin") == null)
            {
                var user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };

                var result = userManager.Create(user, "admin1911");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            if (userManager.FindByName("manager") == null)
            {
                var user = new AppUser();
                user.UserName = "manager";
                user.Email = "manager@gmail.com";
                string userPwd = "manager1911";

                var chkUser = userManager.Create(user, userPwd);
                if (chkUser.Succeeded) //nếu tạo thành công
                {
                    userManager.AddToRole(user.Id, "Manager");
                }
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
    }
}
