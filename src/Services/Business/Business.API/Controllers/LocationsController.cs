using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.API.Requests.Locations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Business.API.Application.Events;
using SaaSEqt.eShop.Services.Business.API.Application.Events.Locations;
using SaaSEqt.eShop.Services.Business.API.Extensions;
using SaaSEqt.eShop.Services.Business.API.Requests;
using SaaSEqt.eShop.Services.Business.API.ViewModel;
using SaaSEqt.eShop.Services.Business.Domain.Model.Security;
using SaaSEqt.eShop.Services.Business.API.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class LocationsController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly BusinessService _businessService;
        private readonly BusinessSettings _settings;
        private readonly IeShopIntegrationEventService _eShopIntegrationEventService;

        public LocationsController(IeShopIntegrationEventService eShopIntegrationEventService,
                              IHostingEnvironment env, IOptionsSnapshot<BusinessSettings> settings, 
                                   BusinessService businessService)
        {
            _eShopIntegrationEventService = eShopIntegrationEventService;
            _env = env;
            _settings = settings.Value;
            _businessService = businessService;
        }

        //GET api/v1/[controller]/sites/{siteId:Guid}/locations/{locationId:Guid}
        [HttpGet]
        [Route("sites/{siteId:Guid}/locations")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Location>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Location>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocationById(Guid siteId, Guid? locationId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = _businessService.FindLocations(siteId);

            if (locationId.HasValue)
            {
                var item = await root.SingleOrDefaultAsync(ci => ci.Id == locationId);

                if (item != null)
                {
                    return Ok(item);
                }

                return NotFound();
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<Location>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //POST api/v1/[controller]
        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        //[Route("[controller]")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProvisionLocation([FromBody]ProvisionLocationRequest provisionLocationRequest)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)BadRequest();
            }

            var location = new Location(provisionLocationRequest.SiteId,
                                         provisionLocationRequest.Name,
                                         provisionLocationRequest.Description,
                                         true);
            
            var newLocation = await _businessService.ProvisionLocation(location);

            //LocationCreatedEvent locationCreatedEvent = new LocationCreatedEvent(newLocation.SiteId,
            //                                                                     newLocation.Id,
            //                                                                     newLocation.Name,
            //                                                                     newLocation.Description);


            //await _eShopIntegrationEventService.PublishThroughEventBusAsync(locationCreatedEvent);

            return CreatedAtAction(nameof(GetLocationById), new { siteId = location.SiteId, locationId = location.Id }, null);
        }

        //POST api/v1/[controller]/address
        [HttpPost]
        [Route("contactinformation")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> SetLocationInformation([FromBody]SetLocationInformationRequest request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            Guid siteId = request.SiteId;
            Guid locationId = request.LocationId;

            await _businessService.UpdateLocationInformation(siteId, 
                                                             locationId,
                                                             request.ContactName,
                                                             request.EmailAddress,
                                                             request.PrimaryTelephone,
                                                             request.SecondaryTelephone);

            return Ok();
        }

        //POST api/v1/[controller]/address
        [HttpPost]
        [Route("address")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> SetLocationAddress([FromBody]SetLocationAddressRequest request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            Guid siteId = request.SiteId;
            Guid locationId = request.Id;
            string streetAddress = request.StreetAddress;
            string city = request.City;
            string stateProvince = request.StateProvince;
            string postalCode = request.PostalCode;
            string countryCode = request.CountryCode;

            await _businessService.SetLocationAddress(siteId, locationId,
                                                                          streetAddress,
                                                                          city, stateProvince, postalCode, countryCode);
            
            return Ok();
        }

        //POST api/v1/[controller]/image
        [HttpPost]
        [Route("image")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> SetLocationImage([FromForm]SetLocationImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            Guid siteId = request.SiteId;
            Guid locationId = request.LocationId;

            string imageFileExtension = Path.GetExtension(request.Image.FileName);
            var webRoot = _env.WebRootPath;

            string dir = Path.Combine(webRoot, siteId.ToString());
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, request.LocationId.ToString() + imageFileExtension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            await _businessService.UpdateLocationImage(
                request.LocationId,
                request.SiteId,
                path
            );


            return Ok();
        }

        //POST api/v1/[controller]/location
        [HttpPost]
        [Route("geolocation")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> SetLocationGeolocation([FromBody]SetLocationGeolocationRequest request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            Guid siteId = request.SiteId;
            Guid locationId = request.Id;
            double latitude = request.Latitude;
            double longitude = request.Longitude;

            await _businessService.SetLocationGeolocation(siteId, locationId, latitude, longitude);


            return Ok();
        }

        //POST api/v1/[controller]/additionalimages
        [HttpPost]
        [Route("additionalimage")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddAdditionalLocationImage([FromForm]AddAdditionalLocationImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            Guid siteId = request.SiteId;
            Guid locationId = request.LocationId;


            string imageFileExtension = Path.GetExtension(request.Image.FileName);
            var webRoot = _env.WebRootPath;
            var path = Path.Combine(webRoot, siteId.ToString(), locationId.ToString() + imageFileExtension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            LocationImage newLocationImage = await _businessService.AddAdditionalLocationImage(siteId, locationId, path);

            //AdditionalLocationImageCreatedEvent additionalLocationImageCreatedEvent = new AdditionalLocationImageCreatedEvent(newLocationImage.SiteId,
            //                                                                                                                    newLocationImage.LocationId,
            //                                                                                                                  newLocationImage.Image);
            
            //await _eShopIntegrationEventService.PublishThroughEventBusAsync(additionalLocationImageCreatedEvent);


            return Ok(newLocationImage);
        }

        private List<Location> ChangeUriPlaceholder(List<Location> locations)
        {
            var baseUri = _settings.LocationPicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            foreach (var item in locations)
            {
                item.FillLocationUrl(baseUri, azureStorageEnabled: azureStorageEnabled);
                if (item.AdditionalLocationImages != null){
                    foreach (var img in item.AdditionalLocationImages)
                    {
                        img.FillLocationImageUrl(_settings.LocationAdditionalPicBaseUrl, azureStorageEnabled: azureStorageEnabled);
                    }
                }
            }

            return locations;
        }
    }
}
