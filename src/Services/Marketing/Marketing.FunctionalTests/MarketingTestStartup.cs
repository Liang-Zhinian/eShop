using Microsoft.AspNetCore.Builder;
using Eva.eShop.Services.Marketing.API;
using Microsoft.Extensions.Configuration;

namespace Marketing.FunctionalTests
{
    public class MarketingTestsStartup : Startup
    {
        public MarketingTestsStartup(IConfiguration env) : base(env)
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
