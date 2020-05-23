using System.IO;
using System.Reflection;
using Demo.API;
using Demo.API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.FunctionalTests
{
    public class DemoScenariosBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(DemoScenariosBase))
              .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<Startup>();

            var testServer = new TestServer(hostBuilder);

            testServer.Host
                .MigrateDbContext<SeqContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    //var settings = services.GetService<IOptions<CatalogSettings>>();
                    //var logger = services.GetService<ILogger<CatalogContextSeed>>();

                    //new CatalogContextSeed()
                    //.SeedAsync(context, env, settings, logger)
                    //.Wait();
                });

            return testServer;
        }

        public static class Get
        {
            public static string Key()
            {
                return $"api/values/key";
            }

        }
    }
}
