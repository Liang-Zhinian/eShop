using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF;
using SaaSEqt.eShop.Services.Catalog.API.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
namespace SaaSEqt.eShop.Services.Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<CatalogContext>((context,services)=>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var settings = services.GetService<IOptions<CatalogSettings>>();
                    var logger = services.GetService<ILogger<CatalogContextSeed>>();

                    new CatalogContextSeed()
                    .SeedAsync(context,env,settings,logger)
                    .Wait();

                })
                .MigrateDbContext<IntegrationEventLogContext>((_,__)=> { })
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
                   //.UseUrls("http://*:8082")
             .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseHealthChecks("/hc")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot("Pics")
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })                
                .Build();    
    }
}