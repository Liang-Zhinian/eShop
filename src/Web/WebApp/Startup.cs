using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMvc.Data;
using WebMvc.Models;
using WebMvc.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using WebMVC.Extensions.Authentication;
using System.Security.Claims;

namespace WebMvc
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var useLoadTest = Configuration.GetValue<bool>("UseLoadTest");
            var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = Configuration.GetValue<string>("CallBackUrl");

            services.AddAuthentication(options =>
                    {
                        //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        //options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    })
                    .AddCookie(options =>
                    {
                        options.ExpireTimeSpan = new TimeSpan(0, 2, 0); // 2 minutes in this example
                    })
                    .AddMicrosoftAccount(Configuration)
                    .AddGitHub(Configuration)
                    .AddIdentityServer4(Configuration)
                     .AddOpenIdConnect("oidc", options =>
                     {
                         options.SignInScheme = "Cookies";

                         options.Authority = identityUrl;
                         options.ClientId = "server.hybrid";
                         options.ClientSecret = "secret";
                         options.ResponseType = "code id_token";

                         options.SaveTokens = true;
                         options.GetClaimsFromUserInfoEndpoint = true;
                         options.RequireHttpsMetadata = false;

                         options.Scope.Clear();
                         options.Scope.Add("openid");
                         options.Scope.Add("profile");
                         options.Scope.Add("email");
                         options.Scope.Add("offline_access");
                         options.Scope.Add("api");

                         options.ClaimActions.Remove("amr");
                         options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
                         options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                         options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);

                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             NameClaimType = "name",
                             RoleClaimType = "role"
                         };

                         options.Events.OnUserInformationReceived = ctx =>
                         {
                             List<AuthenticationToken> tokens = ctx.Properties.GetTokens()
                                 as List<AuthenticationToken>;
                             tokens.Add(new AuthenticationToken()
                             {
                                 Name = "TicketCreated",
                                 Value = DateTime.UtcNow.ToString()
                             });
                             ctx.Properties.StoreTokens(tokens);

                             return Task.CompletedTask;
                         };
                     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
