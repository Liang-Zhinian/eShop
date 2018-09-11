extern alias MySqlConnectorAlias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Business.API.Requests.Locations;
using global::Business.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Business.API.Application.Events;
using SaaSEqt.eShop.Services.Business.API.Application.Events.Locations;
using SaaSEqt.eShop.Services.Business.API.Application.Events.ServiceCategory;
using SaaSEqt.eShop.Services.Business.API.Application.Events.Sites;
using SaaSEqt.eShop.Services.Business.API.Infrastructure.Services;
using SaaSEqt.eShop.Services.Business.API.Requests;
using SaaSEqt.eShop.Services.Business.API.ViewModel;
using SaaSEqt.eShop.Services.Business.Domain.Model.Catalog;
using SaaSEqt.eShop.Services.Business.Domain.Model.Security;
using SaaSEqt.eShop.Services.Business.Infrastructure.Data;

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TestController : Controller
    {
        private readonly ITenantService _tenantService;
        private readonly IIdentityAccessIntegrationEventService _identityAccessIntegrationEventService;
        private readonly IeShopIntegrationEventService _eShopIntegrationEventService;
        private readonly IHostingEnvironment _env;
        private readonly BusinessService _businessService;
        private readonly BusinessSettings _settings;
        private readonly BusinessDbContext _context;
        private readonly ILogger<TestController> _logger;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public TestController(BusinessDbContext context,
            IIdentityAccessIntegrationEventService identityAccessIntegrationEventService,
            ITenantService tenantService,
            IeShopIntegrationEventService eShopIntegrationEventService,
              IHostingEnvironment env,
              IOptionsSnapshot<BusinessSettings> settings,
                              BusinessService businessService,
                                 ICatalogIntegrationEventService catalogIntegrationEventService,
                              ILogger<TestController> logger
        )
        {
            _context = context;
            _identityAccessIntegrationEventService = identityAccessIntegrationEventService;
            _tenantService = tenantService;
            _eShopIntegrationEventService = eShopIntegrationEventService;
            _env = env;
            _settings = settings.Value;
            _businessService = businessService;
            _catalogIntegrationEventService = catalogIntegrationEventService ?? throw new ArgumentNullException(nameof(catalogIntegrationEventService));
            _logger = logger;
        }

        //POST api/v1/[controller]/image
        [HttpPost]
        [Route("UpdateLocationImage")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateLocationImage([FromBody]SetLocationImageRequest_Test request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            //string imageFileExtension = Path.GetExtension(request.FileName);
            //var webRoot = _env.WebRootPath;

            //string dir = Path.Combine(webRoot, request.SiteId.ToString(), request.LocationId.ToString());
            //if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            //var path = Path.Combine(request.SiteId.ToString(), request.LocationId.ToString(), request.FileName);

            await _businessService.UpdateLocationImage(request.SiteId, request.LocationId, request.FileName);

            return Ok();
        }

        //POST api/v1/[controller]/image
        [HttpPost]
        [Route("AddLocationAdditionalImage")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddLocationAdditionalImage([FromBody]AddAdditionalLocationImageRequest_Test request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            //string imageFileExtension = Path.GetExtension(request.FileName);
            //var webRoot = _env.WebRootPath;

            //string dir = Path.Combine(webRoot, request.SiteId.ToString(), request.LocationId.ToString());
            //if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            //var path = Path.Combine(request.SiteId.ToString(), request.LocationId.ToString(), request.FileName);

            await _businessService.AddAdditionalLocationImage(request.SiteId, request.LocationId, request.FileName);

            return Ok();
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("GenerateTestingTenantData")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GenerateTestingTenantData()
        {
            TenantViewModel tenant = new TenantViewModel(Guid.NewGuid(),
                                                        "Chanel",
                                                         "Chanel",
                                                        "test@test.com",
                                                        "123-123-1234",
                                                        "123-123-1234",
                                                        "",
                                                        "",
                                                        "Hongkong",
                                                        "Hongkong",
                                                        "China",
                                                         "");

            StaffViewModel administrator = new StaffViewModel()
            {
                FirstName = "admin",
                LastName = "admin",
                EmailAddress = "admin@test.com",
                PrimaryTelephone = "123-123-1234",
                SecondaryTelephone = "123-123-1234",
                AddressStreetAddress = "",
                AddressCity = "Hongkong",
                AddressPostalCode = "",
                AddressStateProvince = "Hongkong",
                AddressCountryCode = "China"
            };

            var newTenant = _tenantService.ProvisionTenant(tenant, administrator);


            TenantCreatedEvent tenantCreatedEvent = new TenantCreatedEvent(
                newTenant.Id,
                newTenant.Name,
                newTenant.Description
            );

            await _identityAccessIntegrationEventService.SaveEventAndContextChangesAsync(tenantCreatedEvent);

            // Publish through the Event Bus and mark the saved event as published
            await _identityAccessIntegrationEventService.PublishThroughEventBusAsync(tenantCreatedEvent);

            return Ok(newTenant.Id);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("GenerateTestingSiteData")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GenerateTestingSiteData(Guid tenantId)
        {
            var newSite = await CreateSite(tenantId);
            return Ok(newSite.Id);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("GenerateTestingLocationsData")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> GenerateTestingLocationsData(Guid siteId)
        {
            await CreateLocations(siteId);

            //Guid serviceCategoryId = await CreateServiceCategory(newSite.Id);
            //await CreateServiceItems(newSite.Id, serviceCategoryId);
            return Ok();
        }

        private async Task<Site> CreateSite(Guid tenantId)
        {
            Site site = new Site(tenantId, "Chanel", "Chanel", true);

            var newSite = _businessService.ProvisionSite(site).Result;

            //SiteCreatedEvent siteCreatedEvent = new SiteCreatedEvent(newSite.TenantId,
            //                                                         newSite.Id,
            //                                                         newSite.Name,
            //                                                         newSite.Description,
            //                                                         newSite.Active,
            //                                                         newSite.ContactInformation.ContactName,
            //                                                         newSite.ContactInformation.PrimaryTelephone,
            //                                                         newSite.ContactInformation.SecondaryTelephone);


            //await _eShopIntegrationEventService.PublishThroughEventBusAsync(siteCreatedEvent);

            return newSite;
        }

        private async Task CreateLocations(Guid siteId)
        {
            var useCustomizationData = _settings.UseCustomizationData;
            var contentRootPath = _env.ContentRootPath;
            var picturePath = _env.WebRootPath;

            var locations = GetLocationsFromFile(siteId, contentRootPath, _context, _logger);

            IList<Location> newLocations = new List<Location>();

            var client = new HttpClient();

            foreach (var location in locations)
            {

                //var newLocation = await _businessService.ProvisionLocation(location);
                //newLocations.Add(newLocation);

                ProvisionLocationRequest provisionLocationRequest = new ProvisionLocationRequest() { 
                    Name = location.Name,
                    Description = location.Description,
                    SiteId = siteId
                };
                string jsonInString = Newtonsoft.Json.JsonConvert.SerializeObject(provisionLocationRequest);
                await client.PostAsync("http://localhost/api/v1/locations", new StringContent(jsonInString, Encoding.UTF8, "application/json"));


            }

            var locations2 = GetLocationsFromFile2(siteId, contentRootPath, _context, _logger);
            foreach (var location in locations2)
            {
                SetLocationAddressRequest setLocationAddressRequest = new SetLocationAddressRequest(location.SiteId,
                                                                                                          location.Id,
                                                                                                          location.Address.Street,
                                                                                                          location.Address.City,
                                                                                                          location.Address.StateProvince,
                                                                                                          location.Address.ZipCode,
                                                                                                          location.Address.Country);
                string jsonInString = Newtonsoft.Json.JsonConvert.SerializeObject(setLocationAddressRequest);
                await client.PostAsync("http://localhost/api/v1/locations/address", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                SetLocationGeolocationRequest setLocationGeolocationRequest = new SetLocationGeolocationRequest(location.SiteId,
                                                                                                         location.Id,
                                                                                                                location.Geolocation.Latitude,
                                                                                                                location.Geolocation.Longitude);
                jsonInString = Newtonsoft.Json.JsonConvert.SerializeObject(setLocationGeolocationRequest);
                await client.PostAsync("http://localhost/api/v1/locations/geolocation", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                SetLocationInformationRequest setLocationInformationRequest = new SetLocationInformationRequest(location.SiteId,
                                                                                                                location.Id,
                                                                                                                location.ContactInformation.ContactName,
                                                                                                                location.ContactInformation.EmailAddress,
                                                                                                                location.ContactInformation.PrimaryTelephone,
                                                                                                                location.ContactInformation.SecondaryTelephone);
                jsonInString = Newtonsoft.Json.JsonConvert.SerializeObject(setLocationInformationRequest);
                await client.PostAsync("http://localhost/api/v1/locations/contactinformation", new StringContent(jsonInString, Encoding.UTF8, "application/json"));


                //IFormFile formFile = new FormFile()
                SetLocationImageRequest_Test setLocationImageRequest = new SetLocationImageRequest_Test(location.SiteId,
                                                                                              location.Id,
                                                                                              location.Image);
                jsonInString = Newtonsoft.Json.JsonConvert.SerializeObject(setLocationImageRequest);
                await client.PostAsync("http://localhost/api/v1/Test/UpdateLocationImage", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                AddAdditionalLocationImageRequest_Test addAdditionalLocationImageRequest = new AddAdditionalLocationImageRequest_Test(location.SiteId,
                                                                                                                            location.Id,
                                                                                                                            location.AdditionalLocationImages.ElementAtOrDefault(0).Image);
                jsonInString = Newtonsoft.Json.JsonConvert.SerializeObject(addAdditionalLocationImageRequest);
                await client.PostAsync("http://localhost/api/v1/Test/AddLocationAdditionalImage", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            }

        }

        private async Task<Guid> CreateServiceCategory(Guid siteId){
            var newCategory = new ServiceCategory(siteId, "Appointment", "Appointment", true, ScheduleType.Appointment.Id);
            _context.ServiceCategories.Add(newCategory);

            ServiceCategoryCreatedEvent serviceCategoryCreatedEvent = new ServiceCategoryCreatedEvent(newCategory.SiteId,
                                                                                                      newCategory.Id,
                                                                                                      newCategory.Name,
                                                                                                      newCategory.Description,
                                                                                                      newCategory.AllowOnlineScheduling,
                                                                                                      newCategory.ScheduleTypeId);


            await _catalogIntegrationEventService.PublishThroughEventBusAsync(serviceCategoryCreatedEvent);

            return newCategory.Id;
        }

        private async Task CreateServiceItems(Guid siteId, Guid serviceCategoryId)
        {
            var items = GetPreconfiguredServiceItems();
            foreach (var newItem in items)
            {
                newItem.SiteId = siteId;
                newItem.ServiceCategoryId = serviceCategoryId;

                _context.ServiceItems.Add(newItem);

                ServiceItemCreatedEvent serviceItemCreatedEvent = new ServiceItemCreatedEvent(newItem.SiteId,
                                                                                              newItem.Id,
                                                                                              newItem.Name,
                                                                                              newItem.Description,
                                                                                              newItem.DefaultTimeLength,
                                                                                              newItem.Price,
                                                                                              newItem.AllowOnlineScheduling,
                                                                                              newItem.ServiceCategoryId,
                                                                                              newItem.IndustryStandardCategoryName,
                                                                                              newItem.IndustryStandardSubcategoryName);


                await _catalogIntegrationEventService.PublishThroughEventBusAsync(serviceItemCreatedEvent);


            }
        }

        private IEnumerable<Location> GetLocationsFromFile(Guid siteId, string contentRootPath, BusinessDbContext context, ILogger<TestController> logger)
        {
            string csvFileLocations = Path.Combine(contentRootPath, "Setup", "locations.csv");

            if (!System.IO.File.Exists(csvFileLocations))
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

            //var siteIdLookup = context.Sites.ToDictionary(ct => ct.Name, ct => ct.Id);
            //var catalogBrandIdLookup = context.CatalogBrands.ToDictionary(ct => ct.Brand, ct => ct.Id);

            return System.IO.File.ReadAllLines(csvFileLocations, System.Text.Encoding.Default)
                        .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                         .SelectTry(column => CreateLocation(column, csvheaders, siteId, contentRootPath, logger))
                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                        .Where(x => x != null);
        }

        private Location CreateLocation(string[] column, string[] headers, Guid siteId, string contentRootPath, ILogger<TestController> logger)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            for (int i = 0; i < column.Length; i++)
            {
                logger.LogDebug(column[i]);
            }

            var location = new Location(siteId,
                                        column[Array.IndexOf(headers, "storename")].Trim('"').Trim(),
                                        column[Array.IndexOf(headers, "storecode")].Trim('"').Trim(),
                                        true);

            return location;
        }

        private IEnumerable<Location> GetLocationsFromFile2(Guid siteId, string contentRootPath, BusinessDbContext context, ILogger<TestController> logger)
        {
            string csvFileLocations = Path.Combine(contentRootPath, "Setup", "locations.csv");

            if (!System.IO.File.Exists(csvFileLocations))
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

            //var siteIdLookup = context.Sites.ToDictionary(ct => ct.Name, ct => ct.Id);
            //var catalogBrandIdLookup = context.CatalogBrands.ToDictionary(ct => ct.Brand, ct => ct.Id);

            return System.IO.File.ReadAllLines(csvFileLocations, System.Text.Encoding.Default)
                            .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                         .SelectTry(column => UpdateLocation(context, column, csvheaders, siteId, contentRootPath))
                        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                        .Where(x => x != null);
        }

        private Location UpdateLocation(BusinessDbContext context, string[] column, string[] headers, Guid siteId, string contentRootPath)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            var location = context.Locations.Where(y => y.SiteId.Equals(siteId) && y.Name.Equals(column[Array.IndexOf(headers, "storename")].Trim('"').Trim())).SingleOrDefault();

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

            var dir = siteId.ToString() + "/" + location.Id.ToString();
            var abs_dir = Path.Combine(contentRootPath + "/Pics/", dir);
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

            location.ChangeImage(dir + "/" + fileName);

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

            LocationImage locationImage = new LocationImage(siteId, location.Id, dir + "/" + fileName);
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
            string[] csvheaders = System.IO.File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

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

        private IEnumerable<ServiceCategory> GetPreconfiguredServiceCategories()
        {
            return new List<ServiceCategory>()
            {
                new ServiceCategory(Guid.Empty,"Appointment","Appointment",true,ScheduleType.Appointment.Id)
            };
        }

        private IEnumerable<ServiceItem> GetPreconfiguredServiceItems()
        {
            return new List<ServiceItem>()
            {
                new ServiceItem(Guid.Empty,
                                "Sublimage Treatment (90 mins)",
                                "Sublimage Treatment (90 mins)",
                               90,
                               150,
                                Guid.Empty,
                                "Face treatments",
                                "Facial"),
                new ServiceItem(Guid.Empty,
                                "Sublimage Treatment (60 mins)",
                                "Sublimage Treatment (60 mins)",
                               60,
                               120,
                                Guid.Empty,
                                "Face treatments",
                                "Facial"),
                new ServiceItem(Guid.Empty,
                                "Le List Treatment (75 mins)",
                                "Le List Treatment (75 mins)",
                               75,
                               120,
                                Guid.Empty,
                                "Face treatments",
                                "Facial"),
                new ServiceItem(Guid.Empty,
                                "Le Blac Treatment (75 mins)",
                                "Le Blac Treatment (75 mins)",
                               75,
                               120,
                                Guid.Empty,
                                "Face treatments",
                                "Facial"),
            };
        }
    }
}
