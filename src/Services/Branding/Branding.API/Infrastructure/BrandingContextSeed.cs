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

                if (!context.Versions.Any())
                {
                    await context.Versions.AddRangeAsync(GetPreconfiguredVersions());

                    await context.SaveChangesAsync();
                }

                if (!context.CategoryIcons.Any())
                {
                    await context.CategoryIcons.AddRangeAsync(GetPreconfiguredCategoryIcons());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<Model.Version> GetPreconfiguredVersions()
        {
            return new List<Model.Version>()
            {
                new Model.Version{Type = "Beauty", VersionNumber = 1, Language = "CN"},
                new Model.Version{Type = "Fitness", VersionNumber = 1, Language="CN"},
                new Model.Version{Type = "Wellness", VersionNumber = 1, Language="CN"},
                new Model.Version{Type = "Beauty", VersionNumber = 1, Language="EN"},
                new Model.Version{Type = "Fitness", VersionNumber = 1, Language="EN"},
                new Model.Version{Type = "Wellness", VersionNumber = 1, Language="EN"},
            };
        }

        private IEnumerable<CategoryIcon> GetPreconfiguredCategoryIcons()
        {
            return new List<CategoryIcon>()
            {
                new CategoryIcon
                {
                    Name = "Face treatments",
                    IconFileName = "EN_Beauty_Face treatmens_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Face treatmens",
                    Type = "Beauty",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },
                new CategoryIcon
                {
                    Name = "Hair salon",
                    IconFileName = "EN_Beauty_Hair salon_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Hair salon",
                    Type = "Beauty",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },
                new CategoryIcon
                {
                    Name = "Makeup / lashes / brows",
                    IconFileName = "EN_Beauty_Makeup / lashes / brows_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Makeup / lashes / brows",
                    Type = "Beauty",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },
                new CategoryIcon
                {
                    Name = "Nails",
                    IconFileName = "EN_Beauty_Nails_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Nails",
                    Type = "Beauty",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },
                new CategoryIcon
                {
                    Name = "Tanning",
                    IconFileName = "EN_Beauty_Tanning_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Tanning",
                    Type = "Beauty",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },

                // Fitness:
                new CategoryIcon
                {
                    Name = "Dance",
                    IconFileName = "EN_Fitness_Dance_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Dance",
                    Type = "Fitness",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },
                new CategoryIcon
                {
                    Name = "Gymnastics",
                    IconFileName = "EN_Fitness_Gymnastics_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Gymnastics",
                    Type = "Fitness",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },
                new CategoryIcon
                {
                    Name = "Martial arts",
                    IconFileName = "EN_Fitness_Martial arts_General_1.png",
                    SubcategoryName = "General",
                    CategoryName = "Martial arts",
                    Type = "Fitness",
                    Order = 1,
                    VersionNumber = 1,
                    Width = 400,
                    Height = 400,
                    Language = "EN"
                },
            };
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
