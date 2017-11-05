using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace InvestmentTracker.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configuration.DependencyResolver = TypeConfig.RegisterTypes();
        }
    }
}