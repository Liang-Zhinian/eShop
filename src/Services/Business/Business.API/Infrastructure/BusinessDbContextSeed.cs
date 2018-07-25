extern alias MySqlConnectorAlias;

namespace SaaSEqt.eShop.Services.Business.API.Infrastructure.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Polly;
    using SaaSEqt.eShop.Services.Business.Infrastructure;
    using SaaSEqt.eShop.Services.Business.Model;

    public class BusinessDbContextSeed
    {
        public async Task SeedAsync(BusinessDbContext context,IHostingEnvironment env,IOptions<BusinessSettings> settings,ILogger<BusinessDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(BusinessDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;
                var picturePath = env.WebRootPath;

                if (!context.Sites.Any())
                {
                    await context.Sites.AddRangeAsync(GetPreconfiguredSites());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<Site> GetPreconfiguredSites()
        {
            return new List<Site>()
            {
                new Site(Guid.NewGuid(), "Chanel", "Chanel", true)
            };
        }

        private Policy CreatePolicy( ILogger<BusinessDbContextSeed> logger, string prefix,int retries = 3)
        {
            return Policy.Handle<MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}
