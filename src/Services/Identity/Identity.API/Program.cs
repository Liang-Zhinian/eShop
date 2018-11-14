using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SaaSEqt.eShop.Services.Identity.API.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Identity;
using SaaSEqt.eShop.Services.Identity.API.Models;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Serilog;

namespace SaaSEqt.eShop.Services.Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
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
                .MigrateDbContext<ConfigurationDbContext>((context,services)=> 
                {
                    var configuration = services.GetService<IConfiguration>();

                    new ConfigurationDbContextSeed()
                        .SeedAsync(context, configuration)
                        .Wait();
                }).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseHealthChecks("/hc")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                   .ConfigureAppConfiguration((builderContext, builder) =>
                {
                    builder.AddEnvironmentVariables();
                    //var config = builder.Build();
                    //var tokenProvider = new AzureServiceTokenProvider();
                    //var kvClient = new KeyVaultClient((authority, resource, scope) => tokenProvider.KeyVaultTokenCallback(authority, resource, scope));

                    //builder.AddAzureKeyVault(config["KeyVault:BaseUrl"], kvClient, new DefaultKeyVaultSecretManager());
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseApplicationInsights()

                    //.UseSerilog((ctx, config) =>
                    //{
                    //    config.MinimumLevel.Debug()
                    //        .MinimumLevel.Debug()
                    //        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    //        .MinimumLevel.Override("System", LogEventLevel.Warning)
                    //        .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                    //        .Enrich.FromLogContext();

                    //    if (ctx.HostingEnvironment.IsDevelopment())
                    //    {
                    //        config.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}");
                    //    }
                    //    else if (ctx.HostingEnvironment.IsProduction())
                    //    {
                    //        config.WriteTo.File(@"~/LogFiles/Application/identityserver.txt",
                    //            fileSizeLimitBytes: 1_000_000,
                    //            rollOnFileSizeLimit: true,
                    //            shared: true,
                    //            flushToDiskInterval: TimeSpan.FromSeconds(1));
                    //    }
                    //})
                .Build();
    }
}

