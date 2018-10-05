using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SaaSEqt.eShop.Services.IdentityManagement.API.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Identity;
using SaaSEqt.eShop.Services.IdentityManagement.API.Models;

namespace SaaSEqt.eShop.Services.IdentityManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                //.MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ApplicationDbContext>((context, services) =>
                {
                    var env = services.GetService<IHostingEnvironment>();
                    var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();
                    var settings = services.GetService<IOptions<AppSettings>>();
                    var roleManager = services.GetService<RoleManager<IdentityRole>>();
                    var userManager = services.GetService<UserManager<ApplicationUser>>();

                    new ApplicationDbContextSeed()
                    .SeedAsync(context, roleManager, userManager, env, logger, settings)
                        .Wait();
                })
                //.MigrateDbContext<ConfigurationDbContext>((context,services)=> 
                //{
                //    var configuration = services.GetService<IConfiguration>();

                //    new ConfigurationDbContextSeed()
                //        .SeedAsync(context, configuration)
                //        .Wait();
                //})
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseHealthChecks("/hc")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddEnvironmentVariables();
        })
                .ConfigureAppConfiguration(cb =>
                {
                    var sources = cb.Sources;
                    sources.Insert(3, new Microsoft.Extensions.Configuration.Json.JsonConfigurationSource()
                    {
                        Optional = true,
                        Path = "appsettings.localhost.json",
                        ReloadOnChange = false
                    });
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseApplicationInsights()
                .Build();
    }
}

