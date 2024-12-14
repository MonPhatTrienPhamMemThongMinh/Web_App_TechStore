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
                name: "Chatbot",
                url: "Chatbot/{action}/{id}",
                defaults: new { controller = "Chatbot", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Payment",
                url: "Payment/{action}/{id}",
                defaults: new { controller = "Payment", action = "VnPayReturn", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Dialogflow",
                url: "api/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}
