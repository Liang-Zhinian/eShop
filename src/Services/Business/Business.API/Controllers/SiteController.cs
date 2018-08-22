using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Business.API.Extensions;
using SaaSEqt.eShop.Business.API.Requests;
using SaaSEqt.eShop.Business.Infrastructure;
using SaaSEqt.eShop.Business.Infrastructure.Services;
using SaaSEqt.eShop.Business.Domain.Model.Security;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SiteController : Controller
    {
        private readonly IHostingEnvironment _env;
        private BusinessService _businessService;
        private readonly BusinessSettings _settings;

        public SiteController(IHostingEnvironment env, IOptionsSnapshot<BusinessSettings> settings, BusinessService businessService)
        {
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

            await _businessService.ProvisionSite(site);

            return CreatedAtAction(nameof(GetSiteById), new { id = site.Id }, null);
        }
    }
}
