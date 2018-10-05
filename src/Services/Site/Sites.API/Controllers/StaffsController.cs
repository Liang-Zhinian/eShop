using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
using SaaSEqt.eShop.Services.Sites.API.Model;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;
using Sites.API.IntegrationEvents;

namespace SaaSEqt.eShop.Services.Sites.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public partial class StaffsController : Controller
    {
        private readonly SitesContext _sitesContext;
        private readonly IHostingEnvironment _env;
        private readonly SitesSettings _settings;
        private readonly ISitesIntegrationEventService _sitesIntegrationEventService;

        public StaffsController(SitesContext context, ISitesIntegrationEventService eShopIntegrationEventService,
                                   IHostingEnvironment env, IOptionsSnapshot<SitesSettings> settings)
        {
            _sitesContext = context ?? throw new ArgumentNullException(nameof(context));
            _sitesIntegrationEventService = eShopIntegrationEventService;
            _env = env;
            _settings = settings.Value;
        }

        // GET api/v1/[controller]?userName={userName:minlength(1)}
        [HttpGet]
        //[Route("sites/{siteId:Guid}/staffs")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StaffViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Staffs([FromQuery]string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var staff = await _sitesContext.Staffs
                                           .Include(y => y.Site)
                                           .SingleOrDefaultAsync(y => y.EmailAddress.Equals(userName));

            if (staff == null) return NotFound();

            return Ok(staff);
        }

        // GET api/v1/[controller]/sites/{siteId:Guid}/staffs?userName={userName:minlength(1)}
        [HttpGet]
        [Route("sites/{siteId:Guid}/staffs")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StaffViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Staffs(Guid siteId, [FromQuery]string userName)
        {
            if (siteId == Guid.Empty || string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var staff = await _sitesContext.Staffs.SingleOrDefaultAsync(y => y.SiteId.Equals(siteId) && y.EmailAddress.Equals(userName));

            if (staff == null) return NotFound();

            return Ok(staff);
        }

        // POST api/v1/[controller]
        [HttpGet]
        [Route("sites/{siteId:Guid}/locations/{locationId:Guid}/asigned-staffs")]
        [ProducesResponseType(typeof(IEnumerable<Staff>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStaffsAsignedToLocation(Guid siteId, Guid locationId)
        {
            var staffLoginLocation_StaffIds = _sitesContext.StaffLoginLocations
                                                                    .Where(y => y.SiteId.Equals(siteId)
                                                                           && y.LocationId.Equals(locationId))
                                                           .Select(y => y.StaffId);

            var root = await _sitesContext.Staffs
                                    .Where(y => staffLoginLocation_StaffIds.Contains(y.Id))
                                    .OrderBy(y => y.FirstName)
                                          .ToListAsync();



            //var totalItems = await root.LongCountAsync();

            //var itemsOnPage = await root.OrderBy(c => c.Distance)
            //                    .Skip(pageSize * pageIndex)
            //                    .Take(pageSize)
            //                      .ToListAsync();

            //itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            //var model = new PaginatedItemsViewModel<Staff>(
            //pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(root);
        }


        // PUT api/v1/[controller]/sites/guid/staffs/guid
        [HttpPut("sites/{siteId:Guid}/staffs/{staffId:Guid}")]
        public void Put(Guid tenantId, Guid staffId, [FromBody]string value)
        {
        }

        // DELETE api/v1/[controller]/sites/guid/staffs/guid
        [HttpDelete("sites/{siteId:Guid}/staffs/{staffId:Guid}")]
        public void Delete(Guid tenantId, Guid staffId)
        {
        }
    }
}
