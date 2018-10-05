using Sites.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
//using SaaSEqt.eShop.Services.Sites.API.IntegrationEvents.Events;
using SaaSEqt.eShop.Services.Sites.API.Model;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Sites.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace SaaSEqt.eShop.Services.Sites.API.Controllers
{
    //[Route("api/v1/business-information")]
    //[Authorize]
    public partial class BusinessController // : ControllerBase
    {
        //private readonly SitesContext _sitesContext;
        //private readonly SitesSettings _settings;
        //private readonly ISitesIntegrationEventService _sitesIntegrationEventService;

        //public SitesController(SitesContext context, IOptionsSnapshot<SitesSettings> settings, ISitesIntegrationEventService sitesIntegrationEventService)
        //{
        //    _sitesContext = context ?? throw new ArgumentNullException(nameof(context));
        //    _sitesIntegrationEventService = sitesIntegrationEventService ?? throw new ArgumentNullException(nameof(sitesIntegrationEventService));

        //    _settings = settings.Value;
        //    ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //}

        [HttpGet]
        [Route("sites/{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Site), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSiteById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var site = await _sitesContext.Sites.SingleOrDefaultAsync(y => y.Id.Equals(id));
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
        [Route("sites")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ProvisionSite([FromBody]Site site)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)BadRequest();
            }

            _sitesContext.Sites.Add(site);
            await _sitesContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSiteById), new { id = site.Id }, null);
        }

        // POST api/v1/[controller]
        [HttpPost]
        [Route("sites/{siteId:Guid}/locations/{locationId:Guid}/staffs/{staffId:Guid}/asign")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AsignStaffToLocation(Guid siteId, Guid staffId, Guid locationId)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)BadRequest();
            }
            StaffLoginLocation staffLoginLocation = new StaffLoginLocation(siteId, staffId, locationId);

            _sitesContext.StaffLoginLocations.Add(staffLoginLocation);
            await _sitesContext.SaveChangesAsync();

            return Ok();
        }
    }
}
