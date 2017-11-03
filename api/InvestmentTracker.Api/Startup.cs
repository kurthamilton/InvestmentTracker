using InvestmentTracker.ApplicationService.Prices;
using InvestmentTracker.Domain.Prices;
using InvestmentTracker.Persistence.Prices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace InvestmentTracker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRouting();

            AddApplicationServices(services);
            AddPersistenceServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IPriceApplicationService, PriceApplicationService>();
        }

        private static void AddPersistenceServices(IServiceCollection services)
        {
            // TODO - move into settings
            string filePath = GetAppDataFilePath("Prices.csv");

            services.AddTransient<IPricesRepository, IPricesRepository>(x => new PricesRepository(filePath));
        }

        private static string GetAppDataFilePath(string fileName)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string folder = Path.GetDirectoryName(path);

            return Path.Combine(folder, "App_Data", fileName);
        }
    }
}
