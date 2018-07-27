﻿using System;
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

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class LocationsController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly BusinessService _businessService;

        public LocationsController(IHostingEnvironment env,BusinessService businessService)
        {
            _env = env;
            _businessService = businessService;
        }

        //GET api/v1/[controller]
        [HttpGet]
        [Route("[controller]/{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Location), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocationById(Guid siteId, Guid locationId)
        {
            if (siteId == Guid.Empty || locationId == Guid.Empty)
            {
                return BadRequest();
            }

            var site = await _businessService.FindExistingLocation(siteId, locationId);

            if (site != null)
            {
                return Ok(site);
            }

            return NotFound();
        }

        //POST api/v1/[controller]
        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("[controller]")]
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
        [Route("[controller]/address")]
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
        [Route("[controller]/image")]
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
            var path = Path.Combine(webRoot, siteId.ToString(), request.Id.ToString() + "." + imageFileExtension);

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
        [Route("[controller]/location")]
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
        [Route("[controller]/additionalimages")]
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
            var path = Path.Combine(webRoot, siteId.ToString(), locationId.ToString() + "." + imageFileExtension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            await _businessService.AddAdditionalLocationImage(siteId, locationId, path);

            return CreatedAtAction(nameof(GetLocationById), new { siteId = siteId, locationId = locationId }, null);
        }
    }
}
