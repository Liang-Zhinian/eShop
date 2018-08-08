extern alias MySqlConnectorAlias;

namespace SaaSEqt.eShop.Services.Branding.API.Infrastructure
{
    using Microsoft.Extensions.Logging;
    using global::Branding.API.Extensions;
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

    public class BrandingContextSeed
    {
        public async Task SeedAsync(BrandingContext context,IHostingEnvironment env,IOptions<BrandingSettings> settings,ILogger<BrandingContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(BrandingContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;
                var picturePath = env.WebRootPath;

                //if (!context.ScheduleTypes.Any())
                //{
                //    await context.ScheduleTypes.AddRangeAsync(GetPreconfiguredScheduleTypes());

                //    await context.SaveChangesAsync();
                //}
            });
        }

        private IEnumerable<Model.Version> GetPreconfiguredVersions()
        {
            return new List<Model.Version>()
            {
                new Model.Version{Type = "Beauty", VersionNumber = 1},
                new Model.Version{Type = "Fitness", VersionNumber = 1},
                new Model.Version{Type = "Wellness", VersionNumber = 1}
            };
        }


        private IEnumerable<CategoryIcon> GetPreconfiguredCategoryIcons()
        {
            return new List<CategoryIcon>()
            {
                new CategoryIcon
                {
                    Name = "Face_Treatments",
                    IconFileName = "1-Face_Treatments_EN.png",
                    ServiceCategoryId = request.ServiceCategoryId,
                    ServiceCategoryName = request.ServiceCategoryName,
                    Type = request.Type,
                    Order = request.Order,
                    VersionNumber = currentVersion.VersionNumber,
                    Width = size.Width,
                    Height = size.Height,
                    Language = request.Language
                }
            }
        }

        private Policy CreatePolicy( ILogger<BrandingContextSeed> logger, string prefix,int retries = 3)
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
