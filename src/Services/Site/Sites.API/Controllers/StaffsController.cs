using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Sites.API.Extensions;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
using SaaSEqt.eShop.Services.Sites.API.Model;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;
using Sites.API.IntegrationEvents;
using Sites.API.Requests;

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
                                           .Include(y => y.StaffLoginLocations)
                                                .ThenInclude(y=>y.Location)
                                           .SingleOrDefaultAsync(y => y.EmailAddress.Equals(userName));

            if (staff == null) return NotFound();

            var baseUri = _settings.StaffPicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            staff.FillStaffImageUrl(baseUri, azureStorageEnabled);

            //staff.StaffLoginLocations.Select(y => y.Location.FillLocationUrl(_settings.LocationPicBaseUrl, azureStorageEnabled));

            return Ok(staff);
        }

        // GET api/v1/[controller]/sites/{siteId:Guid}/staffs?userName={userName:minlength(1)}
        [HttpGet]
        [Route("sites/{siteId:Guid}/staffs")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StaffViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Staff>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Staffs(Guid siteId, [FromQuery]string userName)
        {
            if (siteId == Guid.Empty)
            {
                return BadRequest();
            }

            var root = _sitesContext.Staffs
                                          .Where(y => siteId.Equals(y.SiteId))
                                    .OrderBy(y => y.FirstName);

            if (!string.IsNullOrEmpty(userName))
            {

                var staff = await root.SingleOrDefaultAsync(y => y.EmailAddress.Equals(userName));

                if (staff == null) return NotFound();


                var baseUri = _settings.StaffPicBaseUrl;
                var azureStorageEnabled = _settings.AzureStorageEnabled;

                staff.FillStaffImageUrl(baseUri, azureStorageEnabled);

                return Ok(staff);
            }

            var staffs = await root.ToListAsync();
            staffs = ChangeStaffImageUriPlaceholder(staffs);

            return Ok(staffs);
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


            return Ok(root);
        }


        // POST api/v1/[controller]/sites/{siteId:Guid}/staffs/{staffId:Guid}/avatar
        [HttpPost]
        [Route("sites/{siteId:Guid}/staffs/{staffId:Guid}/avatar")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Staff), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Staffs(Guid siteId, Guid staffId, [FromForm]UploadImageRequest avatar)
        {
            if (siteId == Guid.Empty || staffId == Guid.Empty)
            {
                return BadRequest();
            }

            var staff = await _sitesContext.Staffs.SingleOrDefaultAsync(y => y.SiteId.Equals(siteId)
                                                                        && y.Id.Equals(staffId));
            if (staff == null) return NotFound();

            string imageFileExtension = Path.GetExtension(avatar.Image.FileName);
            var webRoot = _env.WebRootPath;

            string dir = Path.Combine(webRoot, siteId.ToString());
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, staffId.ToString() + imageFileExtension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await avatar.Image.CopyToAsync(stream);
            }

            string img = Path.Combine(siteId.ToString(), staffId.ToString() + imageFileExtension);
            staff.UpdateImage(img);

            _sitesContext.Staffs.Update(staff);
            await _sitesContext.SaveChangesAsync();

            var baseUri = _settings.StaffPicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            staff.FillStaffImageUrl(baseUri, azureStorageEnabled);

            return Ok(staff);
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


        private List<Staff> ChangeStaffImageUriPlaceholder(List<Staff> staffs)
        {
            var baseUri = _settings.StaffPicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            foreach (var item in staffs)
            {
                item.FillStaffImageUrl(baseUri, azureStorageEnabled: azureStorageEnabled);
            }

            return staffs;
        }
    }
}
