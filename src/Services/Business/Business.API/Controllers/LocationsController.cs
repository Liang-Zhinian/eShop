using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.API.Requests.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.Business.API.Requests;
using SaaSEqt.eShop.Services.Business.API.Infrastructure;
using SaaSEqt.eShop.Services.Business.Infrastructure;
using SaaSEqt.eShop.Services.Business.Model;
using SaaSEqt.eShop.Services.Business.Services;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Business.API.Extensions;
using SaaSEqt.eShop.Services.Business.API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class LocationsController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly BusinessService _businessService;
        private readonly BusinessSettings _settings;

        public LocationsController(IHostingEnvironment env, IOptionsSnapshot<BusinessSettings> settings, BusinessService businessService)
        {
            _env = env;
            _settings = settings.Value;
            _businessService = businessService;
        }

        // GET api/v1/[controller]/test
        [HttpGet]
        [Route("test")]
        public IEnumerable<string> Test()
        {
            return new string[] { "value1", "value2" };
        }

        //POST api/v1/[controller]/GetBusinessLocationsWithinRadius
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Location>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Location>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = await _businessService.GetBusinessLocationsWithinRadius(latitude, longitude, radius, searchText);

            var totalItems = root.LongCount();

            var itemsOnPage = root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<Location>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //GET api/v1/[controller]/ofSiteId/{siteId:Guid}/locationId/{locationId:Guid}
        [HttpGet]
        [Route("ofSiteId/{siteId:Guid}/locationId/{locationId:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Location), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocationById(Guid siteId, Guid locationId)
        {
            if (siteId == Guid.Empty || locationId == Guid.Empty)
            {
                return BadRequest();
            }

            var location = await _businessService.FindExistingLocation(siteId, locationId);
            var baseUri = _settings.LocationPicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;
            location.FillLocationUrl(baseUri, azureStorageEnabled: azureStorageEnabled);

            if (location != null)
            {
                return Ok(location);
            }

            return NotFound();
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
            
            await _businessService.ProvisionLocation(location);

            return CreatedAtAction(nameof(GetLocationById), new { siteId = location.SiteId, locationId = location.Id }, null);
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
            
            return CreatedAtAction(nameof(GetLocationById), new { siteId = siteId, locationId = locationId }, null);
        }

        //POST api/v1/[controller]/image
        [HttpPost]
        [Route("image")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> SetLocationImage(SetLocationImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok(false);
            }

            Guid siteId = request.SiteId;
            Guid locationId = request.Id;

            string imageFileExtension = Path.GetExtension(request.Image.FileName);
            var webRoot = _env.WebRootPath;

            string dir = Path.Combine(webRoot, siteId.ToString());
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, request.Id.ToString() + imageFileExtension);

            using (var stream = new FileStream(path, FileMode.Create))

            {
                await request.Image.CopyToAsync(stream);
            }

            await _businessService.UpdateLocationImage(
                request.Id,
                request.SiteId,
                path
            );


            return CreatedAtAction(nameof(GetLocationById), new { siteId = siteId, locationId = locationId }, null);
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


            return CreatedAtAction(nameof(GetLocationById), new { siteId = siteId, locationId = locationId }, null);
        }

        //POST api/v1/[controller]/additionalimages
        [HttpPost]
        [Route("additionalimage")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddAdditionalLocationImage(AddAdditionalLocationImageRequest request)
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

            await _businessService.AddAdditionalLocationImage(siteId, locationId, path);

            return CreatedAtAction(nameof(GetLocationById), new { siteId = siteId, locationId = locationId }, null);
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
