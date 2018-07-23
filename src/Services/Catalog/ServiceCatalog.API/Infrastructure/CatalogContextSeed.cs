extern alias MySqlConnectorAlias;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure
{
    using Microsoft.Extensions.Logging;
    using global::ServiceCatalog.API.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;
    using Model;
    using Polly;
    using System;
    using System.Collections.Generic;
    //using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using MySql.Data.MySqlClient;

    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext context,IHostingEnvironment env,IOptions<ServiceCatalogSettings> settings,ILogger<CatalogContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CatalogContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;
                var picturePath = env.WebRootPath;

                if (!context.ScheduleTypes.Any())
                {
                    await context.ScheduleTypes.AddRangeAsync(GetPreconfiguredScheduleTypes());

                    await context.SaveChangesAsync();
                }

                if (!context.ServiceCategories.Any())
                {
                    await context.ServiceCategories.AddRangeAsync(GetPreconfiguredServiceCategories());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<ScheduleType> GetPreconfiguredScheduleTypes()
        {
            return new List<ScheduleType>()
            {
                new ScheduleType(1,"All"),
                new ScheduleType(2,"Appointment"),
                new ScheduleType(3,"Resource")
            };
        }


        private IEnumerable<ServiceCategory> GetPreconfiguredServiceCategories()
        {
            return new List<ServiceCategory>()
            {
                new ServiceCategory(Guid.Empty,"Appointment","Appointment",true,ScheduleType.Appointment.Id)
            };
        }

        private Policy CreatePolicy( ILogger<CatalogContextSeed> logger, string prefix,int retries = 3)
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
