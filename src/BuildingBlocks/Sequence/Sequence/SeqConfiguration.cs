// Author: 	Liang Zhinian
// On:		2020/5/24
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sequence.Services;

namespace Sequence
{
    public static class SeqConfiguration
    {
        //public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
               //.SetBasePath(Directory.GetCurrentDirectory())
               //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
               //.Build();

        public static IServiceCollection UseSequence(this IServiceCollection services)
        {
            services.AddTransient<IKeyAllocService, KeyAllocService>();
            return services;
        }

        public static IServiceCollection AddSequenceDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SeqContext>(options =>
            {
                options.UseMySql(connectionString,
                                     mySqlOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(SeqConfiguration).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                // Changing default behavior when client evaluation occurs to throw. 
                // Default in EF Core would be to log a warning when client evaluation is performed.
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            });
            return services;
        }

        //public static IWebHost MigrateSequenceDbContext(this IWebHost webHost)
        //{
        //    webHost.MigrateDbContext<SeqContext>((context, services) =>
        //     {
        //         var env = services.GetService<IHostingEnvironment>();
        //         //var settings = services.GetService<IOptions<CatalogSettings>>();
        //         //var logger = services.GetService<ILogger<CatalogContextSeed>>();

        //         //new CatalogContextSeed()
        //         //.SeedAsync(context, env, settings, logger)
        //         //.Wait();

        //     });

        //    return webHost;
        //}
    }
}
