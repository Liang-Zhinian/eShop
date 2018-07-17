using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.Api.Requests.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.Business.Api.Requests;
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

        //private ProvisionLocationCommand BuildLocationCmd(Guid siteId, 
        //                                                  string name, 
        //                                                  string description
        //                                                 ){
        //    var command = new ProvisionLocationCommand {
        //        Name = name,
        //        Description = description,
        //        ContactName = "test",
        //        EmailAddress = "test@test.com",
        //        PrimaryTelephone = "123-123-1234",
        //        SecondaryTelephone = "123-123-1234",
        //        SiteId = siteId
        //    };

        //    return command;
        //}

        //private async Task CreateTestData(Guid siteId,
        //                                  string locName,
        //                                  string locDesc,
        //                                  byte[] img,
        //                                                  double latitude = 0,
        //                                  double longitude = 0)
        //{ 

        //    var command = BuildLocationCmd(siteId, locName, "Chanel IFC");

        //    var location = await _businessInformationService.ProvisionLocationAsync(command);
        //    await _businessInformationService.SetLocationAddress(location.SiteId, location.Id,
        //                                                         locName, "",
        //                                                   "Hongkong", "Hongkong", "", "China");
        //    await _businessInformationService.SetLocationGeolocation(location.SiteId, location.Id, latitude, longitude);
        //    await _businessInformationService.UpdateLocationImage(new UpdateLocationImageCommand
        //    {
        //        LocationId = location.Id,
        //        SiteId = location.SiteId,
        //        Image = img
        //    });
        //    await _businessInformationService.AddAdditionalLocationImage(location.SiteId, location.Id, img);
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async System.Threading.Tasks.Task<ActionResult> GenerateTestDataAsync()
        {
            var contentRootPath = this._env.ContentRootPath;
            string picFileLogo = Path.Combine(contentRootPath, "Pics", "Chanel logo.png");
            string picFileLocation = Path.Combine(contentRootPath, "Pics", "Location image.png");

            string city = "test";
            string countryCode = "test";
            string postalCode = "test";
            string stateProvince = "test";
            string streetAddress = "test";
            string streetAddress2 = "test";
            double latitude = 0;
            double longitude = 0;

            byte[] logo;
            using (var memoryStream = new MemoryStream())
            {
                (new StreamReader(picFileLogo)).BaseStream.CopyTo(memoryStream);
                logo = memoryStream.ToArray();
            }

            byte[] locImg;
            using (var memoryStream = new MemoryStream())
            {
                (new StreamReader(picFileLocation)).BaseStream.CopyTo(memoryStream);
                locImg = memoryStream.ToArray();
            }

            //var site = _businessInformationQueryService.FindSites().Where(_ => _.Name == "Chanel").FirstOrDefault();

            //await CreateTestData(site.Id, "IFC", "Chanel IFC", locImg, 22.28588, 114.158131);
            //await CreateTestData(site.Id, "HM3", "Chanel Prince's Building", locImg, 22.2812257, 114.159262799999);
            //await CreateTestData(site.Id, "HHP", "Chanel Hysan Place", locImg, 22.2798079, 114.1837883);
            //await CreateTestData(site.Id, "HEM", "Chanel Elements", locImg, 22.3048708, 114.1615219);
            //await CreateTestData(site.Id, "HFW", "Chanel Festival Walk", locImg, 22.3372971, 114.1745273);
            //await CreateTestData(site.Id, "HTP", "Chanel Telford Plaza", locImg, 22.3210652, 114.2132768);
            //await CreateTestData(site.Id, "HNT", "Chanel New Town Plaza", locImg, 22.3814592, 114.1889307);


            return Ok();
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
