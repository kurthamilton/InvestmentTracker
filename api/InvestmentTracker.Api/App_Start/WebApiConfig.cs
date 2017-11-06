using System.Web.Http;
using System.Web.Http.Cors;

namespace InvestmentTracker.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            EnableCorsAttribute corsPolicy = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsPolicy);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    action = RouteParameter.Optional,
                    id = RouteParameter.Optional
                }
            );
        }
    }
}