using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Business.API.Application.Events;
using SaaSEqt.eShop.Services.Business.API.Application.Events.Sites;
using SaaSEqt.eShop.Services.Business.API.Extensions;
using SaaSEqt.eShop.Services.Business.API.Requests;
using SaaSEqt.eShop.Services.Business.Domain.Model.Security;
using SaaSEqt.eShop.Services.Business.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SiteController : Controller
    {
        private readonly IHostingEnvironment _env;
        private BusinessService _businessService;
        private readonly BusinessSettings _settings;
        private readonly IeShopIntegrationEventService _eShopIntegrationEventService;

        public SiteController(IeShopIntegrationEventService eShopIntegrationEventService,
                              IHostingEnvironment env,
                              IOptionsSnapshot<BusinessSettings> settings,
                              BusinessService businessService)
        {
            _eShopIntegrationEventService = eShopIntegrationEventService;
            _env = env;
            _settings = settings.Value;
            _businessService = businessService;
        }

        [HttpGet]
        //[Route("sites/{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Site), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSiteById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var site = await _businessService.FindExistingSite(id);
            var baseUri = _settings.SiteLogoBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;
            site.FillSiteUrl(baseUri, azureStorageEnabled: azureStorageEnabled);

            if (site != null)
            {
                return Ok(site);
            }

            return NotFound();
        }

        // POST api/v1/[controller]
        [HttpPost]
        //[Route("ProvisionSite")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProvisionSite([FromBody]ProvisionSiteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)BadRequest();
            }

            //byte[] logo;
            //using (var memoryStream = new MemoryStream())
            //{
            //    request.Logo.CopyTo(memoryStream);
            //    logo = memoryStream.ToArray();
            //}

            Site site = new Site(request.TenantId, request.Name, request.Description, false);

            var newSite = _businessService.ProvisionSite(site).Result;

            SiteCreatedEvent siteCreatedEvent = new SiteCreatedEvent(newSite.TenantId,
                                                                     newSite.Id,
                                                                     newSite.Name,
                                                                     newSite.Description,
                                                                     newSite.Active,
                                                                     newSite.ContactInformation.ContactName,
                                                                     newSite.ContactInformation.PrimaryTelephone,
                                                                     newSite.ContactInformation.SecondaryTelephone);


            await _eShopIntegrationEventService.PublishThroughEventBusAsync(siteCreatedEvent);


            return CreatedAtAction(nameof(GetSiteById), new { id = site.Id }, null);
        }
    }
}
