using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // the routing system processes routes in the order they are listed. 
            routes.MapRoute(
                name: null, url:"Page{requestedPage}",
                defaults: new { Controller = "Product", action = "List"}
                );

            // this is the original, which i used when i was passing pageid through querystring
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
