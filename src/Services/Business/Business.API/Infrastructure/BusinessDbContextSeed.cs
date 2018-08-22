extern alias MySqlConnectorAlias;

namespace SaaSEqt.eShop.Business.API.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using global::Business.API.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Polly;
    using SaaSEqt.eShop.Business.Infrastructure.Data;
    using SaaSEqt.eShop.Business.Domain.Model.Security;

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

                if (!context.Locations.Any())
                {
                    logger.LogDebug("Seeding locations ...");
                    await context.Locations.AddRangeAsync(GetLocationsFromFile(contentRootPath, context, logger));
                    await context.SaveChangesAsync();

                    context.Locations.UpdateRange(GetLocationsFromFile2(contentRootPath, context, logger));
                    context.SaveChanges();

                    //GetCatalogItemPictures(contentRootPath, picturePath);
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



        private IEnumerable<Location> GetLocationsFromFile(string contentRootPath, BusinessDbContext context, ILogger<BusinessDbContextSeed> logger)
        {
            string csvFileLocations = Path.Combine(contentRootPath, "Setup", "locations.csv");

            if (!File.Exists(csvFileLocations))
            {
                logger.LogDebug("File csvFileLocations does not exists ...");
                return GetPreconfiguredLocations();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "storename", "storecode", "latitude", "longitude", "elevation", "shopaddress", "telephone", "reviewstars" };
                string[] optionalheaders = { "elevation", "reviewstars" };
                csvheaders = GetHeaders(csvFileLocations, requiredHeaders, optionalheaders);
            }
            catch (Exception ex)
            {
                logger.LogDebug("Reading file csvFileLocations errors ...");
                logger.LogError(ex.Message);
                return GetPreconfiguredLocations();
            }

            var siteIdLookup = context.Sites.ToDictionary(ct => ct.Name, ct => ct.Id);
            //var catalogBrandIdLookup = context.CatalogBrands.ToDictionary(ct => ct.Brand, ct => ct.Id);

            return File.ReadAllLines(csvFileLocations, System.Text.Encoding.Default)
                        .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                       .SelectTry(column => CreateLocation(column, csvheaders, siteIdLookup, contentRootPath, logger))
                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                        .Where(x => x != null);
        }

        private Location CreateLocation(string[] column, string[] headers, Dictionary<String, Guid> siteIdLookup, string contentRootPath, ILogger<BusinessDbContextSeed> logger)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            for (int i = 0; i < column.Length; i++)
            {
                logger.LogDebug(column[i]);
            }

            var location = new Location(siteIdLookup["Chanel"],
                                        column[Array.IndexOf(headers, "storename")].Trim('"').Trim(),
                                        column[Array.IndexOf(headers, "storecode")].Trim('"').Trim(),
                                        true);

            return location;
        }

        private IEnumerable<Location> GetLocationsFromFile2(string contentRootPath, BusinessDbContext context, ILogger<BusinessDbContextSeed> logger)
        {
            string csvFileLocations = Path.Combine(contentRootPath, "Setup", "locations.csv");

            if (!File.Exists(csvFileLocations))
            {
                return GetPreconfiguredLocations();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "storename", "storecode", "latitude", "longitude", "elevation", "shopaddress", "telephone", "reviewstars" };
                string[] optionalheaders = { "elevation", "reviewstars" };
                csvheaders = GetHeaders(csvFileLocations, requiredHeaders, optionalheaders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return GetPreconfiguredLocations();
            }

            var siteIdLookup = context.Sites.ToDictionary(ct => ct.Name, ct => ct.Id);
            //var catalogBrandIdLookup = context.CatalogBrands.ToDictionary(ct => ct.Brand, ct => ct.Id);

            return File.ReadAllLines(csvFileLocations, System.Text.Encoding.Default)
                            .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                       .SelectTry(column => UpdateLocation(context, column, csvheaders, siteIdLookup, contentRootPath))
                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                        .Where(x => x != null);
        }

        private Location UpdateLocation(BusinessDbContext context, string[] column, string[] headers, Dictionary<String, Guid> siteIdLookup, string contentRootPath)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            var location = context.Locations.Where(y => y.Name.Equals(column[Array.IndexOf(headers, "storename")].Trim('"').Trim())).SingleOrDefault();

            Address address = new Address(column[Array.IndexOf(headers, "shopaddress")].Trim('"').Trim(),
                                         "Hongkong",
                                         "Hongkong",
                                         "",
                                          "China");
            Geolocation geolocation = new Geolocation(
                double.Parse(column[Array.IndexOf(headers, "latitude")].Trim('"').Trim()),
                double.Parse(column[Array.IndexOf(headers, "longitude")].Trim('"').Trim())
            );

            ContactInformation contactInformation = new ContactInformation("Chanel",
                                                                           column[Array.IndexOf(headers, "telephone")].Trim('"').Trim(),
                                                                           column[Array.IndexOf(headers, "telephone")].Trim('"').Trim(),
                                                                           "support@chanel.hk.com"
                                                                          );

            location.ChangeAddress(address);
            location.ChangeGeolocation(geolocation);
            location.ChangeContactInformation(contactInformation);

            string picFileLogo = Path.Combine(contentRootPath, "Setup", "ChanelLogoMini.png");
            string picFileLocation = Path.Combine(contentRootPath, "Setup", "ChanelLogo@2x.png");


            //byte[] logo;
            //using (var memoryStream = new MemoryStream())
            //{
            //    (new StreamReader(picFileLogo)).BaseStream.CopyTo(memoryStream);
            //    logo = memoryStream.ToArray();
            //}

            var dir = siteIdLookup["Chanel"].ToString() + "/" + location.Id.ToString();
            var abs_dir = Path.Combine(contentRootPath+"/Pics/", dir);
            if (!Directory.Exists(abs_dir)) Directory.CreateDirectory(abs_dir);

            var fileName = "1.png";
            var path = Path.Combine(abs_dir, fileName);

            //using (var memoryStream = new MemoryStream())
            //{
                //(new StreamReader(picFileLogo)).BaseStream.CopyTo(memoryStream);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    (new StreamReader(picFileLogo)).BaseStream.CopyTo(stream);
                    stream.Flush();
                }
            //}

            location.ChangeImage(dir+"/"+fileName);

            fileName = "2.png";
            path = Path.Combine(abs_dir, fileName);

            //using (var memoryStream = new MemoryStream())
            //{
                //(new StreamReader(picFileLocation)).BaseStream.CopyTo(memoryStream);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    (new StreamReader(picFileLocation)).BaseStream.CopyTo(stream);
                    stream.Flush();
                }
            //}

            LocationImage locationImage = new LocationImage(siteIdLookup["Chanel"], location.Id, dir + "/" + fileName);
            location.AddAdditionalImage(locationImage);


            return location;
        }



        private IEnumerable<Location> GetPreconfiguredLocations()
        {
            return new List<Location>()
            {
                //new Location(),

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
