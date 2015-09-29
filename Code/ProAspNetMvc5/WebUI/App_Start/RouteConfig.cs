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

            // - / = lists first page of products from all categories
            routes.MapRoute(null, "", // no expected path
                new
                {
                    // object defaults
                    controller = "Product",
                    action = "List",
                    category = (string)null,
                    page = 1
                });

            // - /Page2 = lists the specified page (Page2) showing items from all categories
            routes.MapRoute(null,
                "Page{page}", // expected path /Page#
                new
                {
                    // object defaults
                    controller = "Product",
                    action = "List",
                    category = (string)null
                },
                new
                {
                    // object constraints
                    page = @"/d+"
                });


            // - /Soccer = lists the first page of items from a specific category (Soccer)    
            routes.MapRoute(null,
                "{category}",// expected path /category
                new
                {
                    // object defaults
                    controller = "Product",
                    action = "List",
                    page = 1
                });


            // - /Soccer/Page2 = shows the specified page (Page2) of items from the specified category (Soccer)
            routes.MapRoute(null,
                "{category}/Page{page}", // expected composited path /category/Page#
                new
                {
                    // object defaults
                    controller = "Products",
                    action = "List"
                },
                new
                {
                    // object constraints - 
                    page = @"/d+"
                });


            // catch all
            routes.MapRoute(null, "{controller}/{action}");// expected only controller/action

            // this is the original, which i used when i was passing pageid through querystring
            // no longer valid since i have no controllers with optional parms for list
            //routes.MapRoute("Default", "{controller}/{action}/{id}", new
            //{
            //    controller = "Product",
            //    action = "List",
            //    id = UrlParameter.Optional
            //}
            //    );
        }
    }
}