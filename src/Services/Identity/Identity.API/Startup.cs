﻿using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.API.Infrastructure.Filters;
using Identity.API.Infrastructure.Middlewares;
using IdentityServer4.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SaaSEqt.eShop.Services.Identity.API.Certificates;
using SaaSEqt.eShop.Services.Identity.API.Configuration;
using SaaSEqt.eShop.Services.Identity.API.Data;
using SaaSEqt.eShop.Services.Identity.API.Models;
using SaaSEqt.eShop.Services.Identity.API.Services;
using SaaSEqt.Infrastructure.HealthChecks.MySQL;
using StackExchange.Redis;

namespace SaaSEqt.eShop.Services.Identity.API
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            RegisterAppInsights(services);

            // Add framework services.
            services.AddCustomMvc(Configuration)
                    .AddCustomAuthentication(Configuration)
                    .AddHttpServices()
                    .AddIdentityServer4Authentication(Configuration);

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddAzureWebAppDiagnostics();
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }

            app.UseCors("identity");

            app.UseMiddleware<SerilogMiddleware>();

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            app.Map("/liveness", lapp => lapp.Run(async ctx => ctx.Response.StatusCode = 200));
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            app.UseStaticFiles();


            // Make work identity server redirections in Edge and lastest versions of browers. WARN: Not valid in a production environment.
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
                await next();
            });

            // Adds IdentityServer
            app.UseIdentityServer();

            app.UseAuthentication();

            SwaggerSupport.ConfigureSwaggerUI(app, Configuration);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvcWithDefaultRoute();
        }

        private void RegisterAppInsights(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            var orchestratorType = Configuration.GetValue<string>("OrchestratorType");

            if (orchestratorType?.ToUpper() == "K8S")
            {
                // Enable K8s telemetry initializer
                services.EnableKubernetes();
            }
            if (orchestratorType?.ToUpper() == "SF")
            {
                // Enable SF telemetry initializer
                services.AddSingleton<ITelemetryInitializer>((serviceProvider) =>
                    new FabricTelemetryInitializer());
            }
        }

    }


    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                                                        options.UseMySql(configuration["ConnectionString"],
                                     mySqlOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     }));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<AppSettings>(configuration);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                    .AddControllersAsServices()
                    .AddJsonOptions(opts =>
                    {
                        opts.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                        opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        //设置时间格式
                        //opts.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    });

            if (configuration.GetValue<string>("IsClusterEnv") == bool.TrueString)
            {
                services.AddDataProtection(opts =>
                {
                    opts.ApplicationDiscriminator = "eshop.identity";
                })
                        .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration["DPConnectionString"]), "DataProtection-Keys");
            }

            services.AddHealthChecks(checks =>
            {
                var minutes = 1;
                if (int.TryParse(configuration["HealthCheck:Timeout"], out var minutesParsed))
                {
                    minutes = minutesParsed;
                }
                checks.AddMySQLCheck("Identity_Db", configuration["ConnectionString"], TimeSpan.FromMinutes(minutes));
            });

            // Add framework services.
            services.AddSwaggerSupport(configuration);

            return services;
        }

        public static IServiceCollection AddIdentityServer4Authentication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();
            services.AddTransient<IRedirectService, RedirectService>();

            var connectionString = configuration["ConnectionString"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Adds IdentityServer
            services.AddIdentityServer(x => x.IssuerUri = "null")
                    .AddSigningCredential(Certificate.Get())
                //.AddTestUsers(null)
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseMySql(connectionString,
                                     mySqlOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(migrationsAssembly);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseMySql(connectionString,
                                    mySqlOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(migrationsAssembly);
                                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
                })
                    .Services.AddTransient<IProfileService, ProfileService>();
                         //.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            //services
                //.AddAuthentication()
                //.AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                //{
                //    options.Authority = identityUrl;

                //    options.ApiName = "identity";
                //    options.ApiSecret = "secret";
                //    options.RequireHttpsMetadata = false;
                //    options.SupportedTokens = SupportedTokens.Both;
                //});

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = identityUrl,
                    ValidateAudience = false,
                    ValidAudience = "identity",
                    ValidateLifetime = true,

                };
            });


            // add CORS policy for non-IdentityServer endpoints
            services.AddCors(options =>
            {
                options.AddPolicy("identity", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            
            return services;
        }
    }
}
