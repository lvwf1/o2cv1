using System.Web.Mvc;
using System.Web.Routing;

namespace O2V1Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute("GetTableColumns",
                           "home/gettablecolumns/",
                           new { controller = "Home", action = "GetTableColumns" },
                           new[] { "O2V1Web.Controllers" });

            routes.MapRoute("GetQueryId",
                         "home/getqueryid/",
                         new { controller = "Home", action = "GetQueryId" },
                         new[] { "O2V1Web.Controllers" });
        }
    }
}
