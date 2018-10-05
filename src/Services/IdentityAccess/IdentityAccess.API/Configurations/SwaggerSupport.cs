﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using SaaSEqt.eShop.Services.IdentityAccess.API.Infrastructure.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace SaaSEqt.eShop.Services.IdentityAccess.API.Configurations
{
    public static class SwaggerSupport
    {
        public static void AddSwaggerSupport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Identity Access HTTP API",
                    Version = "v1",
                    Description = "The Identity Access Microservice HTTP API. This is a Data-Driven/CRUD microservice sample",
                    TermsOfService = "Terms Of Service"
                });

                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                    TokenUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "identityaccess", "Identity Access API" }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();

            });
        }

        public static void ConfigureSwaggerUI(IApplicationBuilder app, IConfiguration configuration)
        {
            string pathBase = configuration["PATH_BASE"];
            app.UseSwagger()
                  .UseSwaggerUI(c =>
                  {
                      c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "IdentityAccess.API V1");
                        c.OAuthClientId("identityaccessswaggerui");
                c.OAuthAppName("Identity Access Swagger UI");
                  });
        }
    }
}
