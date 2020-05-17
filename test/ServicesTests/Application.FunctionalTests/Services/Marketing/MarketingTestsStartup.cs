namespace FunctionalTests.Services.Marketing
{
    using FunctionalTests.Middleware;
    using Microsoft.AspNetCore.Builder;
    using Eva.eShop.Services.Marketing.API;
    using Microsoft.Extensions.Configuration;

    public class MarketingTestsStartup : Startup
    {
        public MarketingTestsStartup(IConfiguration configuration) : base(configuration)
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
