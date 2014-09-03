using System.Web.Mvc;
using System.Web.Routing;

namespace Glimpse.NHibernate.Sample.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "EmployeeInfo", action = "Index", id = UrlParameter.Optional } );
        }
    }
}