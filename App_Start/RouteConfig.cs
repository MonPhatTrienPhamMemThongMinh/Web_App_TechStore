using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAnWebGamingGear
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "DoAnWebGamingGear.Controllers" }
            );

            routes.MapRoute(
                name: "OrderSuccess",
                url: "Payment/OrderSuccess/{orderId}",
                defaults: new { controller = "Payment", action = "OrderSuccess", orderId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Payment",
                url: "Payment/{action}/{id}",
                defaults: new { controller = "Payment", action = "VnPayReturn", id = UrlParameter.Optional }
            );
        }
    }
}
