// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
      //      services.AddMvcCore().AddApiExplorer()
      //.AddAuthorization()
      //.AddJsonFormatters()
      //.AddMvcOptions(options =>
      //{
      //    var policy = new AuthorizationPolicyBuilder("IdSrv", "Auth0")
      //      .RequireAuthenticatedUser()
      //      .Build();
      //    options.Filters.Add(new AuthorizeFilter(policy));
      //});

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            //services.AddAuthorization();
            //services.AddJsonFormatters();


            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Api1",
                    Version = "v1",
                    Description = "Api1",
                    TermsOfService = "Terms Of Service"
                });

                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                    TokenUrl = $"{Configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "api1", "Api1" }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //services.AddAuthentication("Bearer")
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = "http://localhost:5105";
            //    options.RequireHttpsMetadata = false;

            //    options.ApiName = "api1";
            //});

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                //.AddIdentityServerAuthentication(options =>
                //{
                //    options.Authority = "http://localhost:5105";
                //    options.RequireHttpsMetadata = false;

                //    options.ApiName = "api1";
                //});
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:5105";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "api1";
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async ctx =>
                        {
                            int i = 0;
                        },
                        OnTokenValidated = async ctx =>
                        {
                            int i = 0;
                        }
                    };
                // if using JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear()
                // then must set the TokenValidationParameters like below
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }

            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Api V1");
                c.ConfigureOAuth2("Api", "", "", "Api Swagger UI");
            });
        }
    }
}