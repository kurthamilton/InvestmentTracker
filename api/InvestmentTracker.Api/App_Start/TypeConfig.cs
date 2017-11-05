using Autofac;
using Autofac.Integration.WebApi;
using InvestmentTracker.ApplicationService.Prices;
using InvestmentTracker.Domain.Investments;
using InvestmentTracker.Domain.Prices;
using InvestmentTracker.Persistence.Prices;
using InvestmentTracker.Scraper.Investments;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http.Dependencies;

namespace InvestmentTracker.Api
{
    public static class TypeConfig
    {
        public static IDependencyResolver RegisterTypes()
        {
            ILifetimeScope container = new ContainerBuilder()
                          .RegisterApiTypes()
                          .RegisterApplicationServiceTypes()
                          .RegisterPersistenceTypes()
                          .RegisterScraperTypes()
                          .Build();

            return new AutofacWebApiDependencyResolver(container);
        }

        private static string GetAppDataFilePath(string fileName)
        {
            string folder = GetAppDataFolder();
            return Path.Combine(folder, fileName);
        }

        private static string GetAppDataFolder()
        {
            string path = HostingEnvironment.MapPath(@"~/App_Data");
            return path;
        }

        private static ContainerBuilder RegisterApiTypes(this ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            return builder;
        }

        private static ContainerBuilder RegisterApplicationServiceTypes(this ContainerBuilder builder)
        {
            builder.RegisterType<PriceApplicationService>().As<IPriceApplicationService>();

            return builder;
        }

        private static ContainerBuilder RegisterPersistenceTypes(this ContainerBuilder builder)
        {
            string filePath = GetAppDataFilePath("Prices.csv");

            builder.RegisterInstance<IPriceRepository>(new PriceRepository(filePath));

            return builder;
        }

        private static ContainerBuilder RegisterScraperTypes(this ContainerBuilder builder)
        {
            builder.RegisterType<InvestmentFactory>().As<IInvestmentFactory>();

            return builder;
        }
    }
}