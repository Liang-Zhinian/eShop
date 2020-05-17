using FunctionalTests.Middleware;
using Microsoft.AspNetCore.Builder;
using Eva.eShop.Services.Basket.API;
using Microsoft.Extensions.Configuration;

namespace FunctionalTests.Services.Basket
{
    class BasketTestsStartup : Startup
    {
        public BasketTestsStartup(IConfiguration env) : base(env)
        {
        }

        protected override void ConfigureAuth(IApplicationBuilder app)
        {
            if (Configuration["isTest"] == bool.TrueString.ToLowerInvariant())
            {
                app.UseMiddleware<AutoAuthorizeMiddleware>();
            }
            else
            {
                base.ConfigureAuth(app);
            }
        }
    }
}
