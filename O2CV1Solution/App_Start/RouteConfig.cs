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
            routes.MapRoute("GetStatesByCountryId",
                           "address/getstatesbycountryid/",
                           new { controller = "Address", action = "GetStatesByCountryId" },
                           new[] { "CountryStateApplication.Controllers" });
        }
    }
}
