using System.Web.Mvc;
using System.Web.Routing;
using O2.Web;

namespace O2V1Web {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
