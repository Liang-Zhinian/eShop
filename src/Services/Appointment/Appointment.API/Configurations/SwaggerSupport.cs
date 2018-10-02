using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using SaaSEqt.eShop.Services.Appointment.API.Infrastructure.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Appointment.API.Configurations
{
    public static class SwaggerSupport
    {
        public static void AddSwaggerSupport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Book2 Appointment Interface Document",
                    Description = "RESTful Appointment API for Book2",
                    TermsOfService = "None",
                });

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                    TokenUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "locations", "Locations API" }
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }
    }
}
