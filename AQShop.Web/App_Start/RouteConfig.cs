using System.Web.Mvc;
using System.Web.Routing;

namespace AQShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
            name: "Search",
            url: "tim-kiem.html",
            defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
            namespaces: new string[] { "AQShop.Web.Controllers" }
        );
            routes.MapRoute(
     name: "Login",
     url: "dang-nhap.html",
     defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
     namespaces: new string[] { "AQShop.Web.Controllers" }
 );

            routes.MapRoute(
         name: "Register",
         url: "dang-ky.html",
         defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
         namespaces: new string[] { "AQShop.Web.Controllers" }
     );

            routes.MapRoute(
            name: "ConcactDetail",
            url: "lien-he.html",
            defaults: new { controller = "ContactDetail", action = "Index", id = UrlParameter.Optional },
            namespaces: new string[] { "AQShop.Web.Controllers" }
        );

            //  routes.MapRoute(
            //    name: "About",
            //    url: "gioi-thieu.html",
            //    defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { "AQShop.Web.Controllers" }
            //);

            routes.MapRoute(
               name: "Product Category",
               url: "{Alias}.pc-{id}.html",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional }

           );

            routes.MapRoute(
            name: "Page",
            url: "trang/{alias}.html",
            defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
             namespaces: new string[] { "AQShop.Web.Controllers" }
        );

            routes.MapRoute(
              name: "Taglist",
              url: "tag/{tagId}.html",
              defaults: new { controller = "Product", action = "GetListProductByTagId", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "Product",
             url: "{Alias}.p-{id}.html",
             defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}