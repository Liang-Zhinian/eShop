extern alias MySqlConnectorAlias;

namespace SaaSEqt.eShop.Services.Catalog.API.Infrastructure
{
    using Microsoft.Extensions.Logging;
    using global::Catalog.API.Extensions;
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
        private Guid MerchantId = Guid.Parse("aa879796-630b-44a6-bc6c-c0a39853786c");

        private Guid CatalogTypeId1 = Guid.Parse("fe6ed08d-3543-4f60-ae4d-8c3604dcaa62");
        private Guid CatalogTypeId2 = Guid.Parse("660526d4-410e-4cbd-b19b-ef83a502302e");
        private Guid CatalogTypeId3 = Guid.Parse("d28a2ff0-d610-4746-9b79-12c471bd8e6c");
        private Guid CatalogTypeId4 = Guid.Parse("dff7ef75-2086-4416-be7f-de848dd37b92");

        private Guid CatalogBrandId1 = Guid.Parse("8494bcb0-3194-426b-a4a4-3ea3db4b0af8");
        private Guid CatalogBrandId2 = Guid.Parse("b6114cc6-25d4-4aa2-a611-c54b47ea9a97");
        private Guid CatalogBrandId3 = Guid.Parse("5353cd3d-3d79-4b87-a310-5cf7de92d0d4");
        private Guid CatalogBrandId4 = Guid.Parse("e4445fbb-6184-4e14-ac29-f3ea013e3b02");
        private Guid CatalogBrandId5 = Guid.Parse("c84291df-9939-4197-aa05-e54bbb042902");

        public async Task SeedAsync(CatalogContext context,IHostingEnvironment env,IOptions<CatalogSettings> settings,ILogger<CatalogContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CatalogContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;
                var picturePath = env.WebRootPath;

                if (!context.CatalogBrands.Any())
                {
                    await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

                    await context.SaveChangesAsync();
                }

                if (!context.CatalogTypes.Any())
                {
                    await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

                    await context.SaveChangesAsync();
                }

                if (!context.CatalogItems.Any())
                {
                    await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

                    await context.SaveChangesAsync();

                    GetCatalogItemPictures(contentRootPath, picturePath);
                }
            });
        }
        /*
        private IEnumerable<CatalogBrand> GetCatalogBrandsFromFile(string contentRootPath, ILogger<CatalogContextSeed> logger)
        {
            string csvFileCatalogBrands = Path.Combine(contentRootPath, "Setup", "CatalogBrands.csv");

            if (!File.Exists(csvFileCatalogBrands))
            {
                return GetPreconfiguredCatalogBrands();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "catalogbrand" };
                csvheaders = GetHeaders( csvFileCatalogBrands, requiredHeaders );
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return GetPreconfiguredCatalogBrands();
            }

            return File.ReadAllLines(csvFileCatalogBrands)
                                        .Skip(1) // skip header row
                                        .SelectTry(x => CreateCatalogBrand(x))
                                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                                        .Where(x => x != null);
        }*/

        private CatalogBrand CreateCatalogBrand(string brand)
        {
            brand = brand.Trim('"').Trim();

            if (String.IsNullOrEmpty(brand))
            {
                throw new Exception("catalog Brand Name is empty");
            }

            return new CatalogBrand
            {
                Brand = brand,
            };
        }

        private IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand() { Id = CatalogBrandId1, Brand = "Azure", MerchantId=MerchantId },
                new CatalogBrand() { Id = CatalogBrandId2, Brand = ".NET", MerchantId=MerchantId },
                new CatalogBrand() { Id = CatalogBrandId3, Brand = "Visual Studio", MerchantId=MerchantId },
                new CatalogBrand() { Id = CatalogBrandId4, Brand = "SQL Server", MerchantId=MerchantId },
                new CatalogBrand() { Id = CatalogBrandId5, Brand = "Other", MerchantId=MerchantId }
            };
        }
        /*
        private IEnumerable<CatalogType> GetCatalogTypesFromFile(string contentRootPath, ILogger<CatalogContextSeed> logger)
        {
            string csvFileCatalogTypes = Path.Combine(contentRootPath, "Setup", "CatalogTypes.csv");

            if (!File.Exists(csvFileCatalogTypes))
            {
                return GetPreconfiguredCatalogTypes();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "catalogtype" };
                csvheaders = GetHeaders( csvFileCatalogTypes, requiredHeaders );
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return GetPreconfiguredCatalogTypes();
            }

            return File.ReadAllLines(csvFileCatalogTypes)
                                        .Skip(1) // skip header row
                                        .SelectTry(x => CreateCatalogType(x))
                                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                                        .Where(x => x != null);
        }*/

        private CatalogType CreateCatalogType(string type)
        {
            type = type.Trim('"').Trim();

            if (String.IsNullOrEmpty(type))
            {
                throw new Exception("catalog Type Name is empty");
            }

            return new CatalogType
            {
                Type = type,
            };
        }

        private IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType() { Id=CatalogTypeId1, Type = "Mug", MerchantId=MerchantId},
                new CatalogType() { Id=CatalogTypeId2, Type = "T-Shirt", MerchantId=MerchantId },
                new CatalogType() { Id=CatalogTypeId3, Type = "Sheet", MerchantId=MerchantId },
                new CatalogType() { Id=CatalogTypeId4, Type = "USB Memory Stick", MerchantId=MerchantId }
            };
        }
        /*
        private IEnumerable<CatalogItem> GetCatalogItemsFromFile(string contentRootPath, CatalogContext context, ILogger<CatalogContextSeed> logger)
        {
            string csvFileCatalogItems = Path.Combine(contentRootPath, "Setup", "CatalogItems.csv");

            if (!File.Exists(csvFileCatalogItems))
            {
                return GetPreconfiguredItems();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "catalogtypename", "catalogbrandname", "description", "name", "price", "pictureFileName" };
                string[] optionalheaders = { "availablestock", "restockthreshold", "maxstockthreshold", "onreorder" };
                csvheaders = GetHeaders(csvFileCatalogItems, requiredHeaders, optionalheaders );
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return GetPreconfiguredItems();
            }

            var catalogTypeIdLookup = context.CatalogTypes.ToDictionary(ct => ct.Type, ct => ct.Id);
            var catalogBrandIdLookup = context.CatalogBrands.ToDictionary(ct => ct.Brand, ct => ct.Id);

            return File.ReadAllLines(csvFileCatalogItems)
                        .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)") )
                        .SelectTry(column => CreateCatalogItem(column, csvheaders, catalogTypeIdLookup, catalogBrandIdLookup))
                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                        .Where(x => x != null);
        }

        private CatalogItem CreateCatalogItem(string[] column, string[] headers, Dictionary<String, Guid> catalogTypeIdLookup, Dictionary<String, Guid> catalogBrandIdLookup)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            string catalogTypeName = column[Array.IndexOf(headers, "catalogtypename")].Trim('"').Trim();
            if (!catalogTypeIdLookup.ContainsKey(catalogTypeName))
            {
                throw new Exception($"type={catalogTypeName} does not exist in catalogTypes");
            }

            string catalogBrandName = column[Array.IndexOf(headers, "catalogbrandname")].Trim('"').Trim();
            if (!catalogBrandIdLookup.ContainsKey(catalogBrandName))
            {
                throw new Exception($"type={catalogTypeName} does not exist in catalogTypes");
            }

            string priceString = column[Array.IndexOf(headers, "price")].Trim('"').Trim();
            if (!Decimal.TryParse(priceString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out Decimal price))
            {
                throw new Exception($"price={priceString}is not a valid decimal number");
            }

            var catalogItem = new CatalogItem()
            {
                CatalogTypeId = catalogTypeIdLookup[catalogTypeName],
                CatalogBrandId = catalogBrandIdLookup[catalogBrandName],
                Description = column[Array.IndexOf(headers, "description")].Trim('"').Trim(),
                Name = column[Array.IndexOf(headers, "name")].Trim('"').Trim(),
                Price = price,
                PictureUri = column[Array.IndexOf(headers, "pictureuri")].Trim('"').Trim(),
            };

            int availableStockIndex = Array.IndexOf(headers, "availablestock");
            if (availableStockIndex != -1)
            {
                string availableStockString = column[availableStockIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(availableStockString))
                {
                    if ( int.TryParse(availableStockString, out int availableStock))
                    {
                        catalogItem.AvailableStock = availableStock;
                    }
                    else
                    {
                        throw new Exception($"availableStock={availableStockString} is not a valid integer");
                    }
                }
            }

            int restockThresholdIndex = Array.IndexOf(headers, "restockthreshold");
            if (restockThresholdIndex != -1)
            {
                string restockThresholdString = column[restockThresholdIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(restockThresholdString))
                {
                    if (int.TryParse(restockThresholdString, out int restockThreshold))
                    {
                        catalogItem.RestockThreshold = restockThreshold;
                    }
                    else
                    {
                        throw new Exception($"restockThreshold={restockThreshold} is not a valid integer");
                    }
                }
            }

            int maxStockThresholdIndex = Array.IndexOf(headers, "maxstockthreshold");
            if (maxStockThresholdIndex != -1)
            {
                string maxStockThresholdString = column[maxStockThresholdIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(maxStockThresholdString))
                {
                    if (int.TryParse(maxStockThresholdString, out int maxStockThreshold))
                    {
                        catalogItem.MaxStockThreshold = maxStockThreshold;
                    }
                    else
                    {
                        throw new Exception($"maxStockThreshold={maxStockThreshold} is not a valid integer");
                    }
                }
            }

            int onReorderIndex = Array.IndexOf(headers, "onreorder");
            if (onReorderIndex != -1)
            {
                string onReorderString = column[onReorderIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(onReorderString))
                {
                    if (bool.TryParse(onReorderString, out bool onReorder))
                    {
                        catalogItem.OnReorder = onReorder;
                    }
                    else
                    {
                        throw new Exception($"onReorder={onReorderString} is not a valid boolean");
                    }
                }
            }

            return catalogItem;
        }
        */

        private IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem { CatalogTypeId = CatalogTypeId2, CatalogBrandId = CatalogBrandId2, AvailableStock = 100, Description = ".NET Bot Black Hoodie", Name = ".NET Bot Black Hoodie", Price = 19.5M, PictureFileName = "1.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId1, CatalogBrandId = CatalogBrandId2, AvailableStock = 100, Description = ".NET Black & White Mug", Name = ".NET Black & White Mug", Price= 8.50M, PictureFileName = "2.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId2, CatalogBrandId = CatalogBrandId5, AvailableStock = 100, Description = "Prism White T-Shirt", Name = "Prism White T-Shirt", Price = 12, PictureFileName = "3.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId2, CatalogBrandId = CatalogBrandId2, AvailableStock = 100, Description = ".NET Foundation T-shirt", Name = ".NET Foundation T-shirt", Price = 12, PictureFileName = "4.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId3, CatalogBrandId = CatalogBrandId5, AvailableStock = 100, Description = "Roslyn Red Sheet", Name = "Roslyn Red Sheet", Price = 8.5M, PictureFileName = "5.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId2, CatalogBrandId = CatalogBrandId2, AvailableStock = 100, Description = ".NET Blue Hoodie", Name = ".NET Blue Hoodie", Price = 12, PictureFileName = "6.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId2, CatalogBrandId = CatalogBrandId5, AvailableStock = 100, Description = "Roslyn Red T-Shirt", Name = "Roslyn Red T-Shirt", Price = 12, PictureFileName = "7.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId2, CatalogBrandId = CatalogBrandId5, AvailableStock = 100, Description = "Kudu Purple Hoodie", Name = "Kudu Purple Hoodie", Price = 8.5M, PictureFileName = "8.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId1, CatalogBrandId = CatalogBrandId5, AvailableStock = 100, Description = "Cup<T> White Mug", Name = "Cup<T> White Mug", Price = 12, PictureFileName = "9.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId3, CatalogBrandId = CatalogBrandId2, AvailableStock = 100, Description = ".NET Foundation Sheet", Name = ".NET Foundation Sheet", Price = 12, PictureFileName = "10.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId3, CatalogBrandId = CatalogBrandId2, AvailableStock = 100, Description = "Cup<T> Sheet", Name = "Cup<T> Sheet", Price = 8.5M, PictureFileName = "11.png", MerchantId=MerchantId },
                new CatalogItem { CatalogTypeId = CatalogTypeId2, CatalogBrandId = CatalogBrandId5, AvailableStock = 100, Description = "Prism White TShirt", Name = "Prism White TShirt", Price = 12, PictureFileName = "12.png", MerchantId=MerchantId },
            };
        }

        private string[] GetHeaders(string csvfile, string[] requiredHeaders, string[] optionalHeaders = null)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() < requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is bigger then csv header count '{csvheaders.Count()}' ");
            }

            if (optionalHeaders != null)
            {
                if (csvheaders.Count() > (requiredHeaders.Count() + optionalHeaders.Count()))
                {
                    throw new Exception($"csv header count '{csvheaders.Count()}'  is larger then required '{requiredHeaders.Count()}' and optional '{optionalHeaders.Count()}' headers count");
                }
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }

        private void GetCatalogItemPictures(string contentRootPath, string picturePath)
        {
            DirectoryInfo directory = new DirectoryInfo(picturePath);
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            string zipFileCatalogItemPictures = Path.Combine(contentRootPath, "Setup", "CatalogItems.zip");
            ZipFile.ExtractToDirectory(zipFileCatalogItemPictures, picturePath);
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
