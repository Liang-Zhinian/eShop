using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Sites.API;
using SaaSEqt.eShop.Services.Sites.API.Extensions;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Services;
using SaaSEqt.eShop.Services.Sites.API.Model.Catalog;
using SaaSEqt.eShop.Services.Sites.API.Model;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sites.API.Controllers
{
    /*
SiteService.GetActivationCode
SiteService.GetLocations
SiteService.GetPrograms
SiteService.GetResourceSchedule
SiteService.GetResources
SiteService.GetSessionTypes
SiteService.GetSites
SiteService.ReserveResource
    */
    [Route("api/v1/[controller]")]
    public class SiteController : Controller
    {
        private readonly SitesDbContext _siteDbContext;
        private readonly IHostingEnvironment _env;
        private readonly BusinessSettings _settings;

        public SiteController(IHostingEnvironment env,
                                   IOptionsSnapshot<BusinessSettings> settings,
                                   SitesDbContext siteDbContext)
        {
            _env = env;
            _settings = settings.Value;
            _siteDbContext = siteDbContext;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Site>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Site>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSites(string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<Site>)_siteDbContext.Sites;

            if (!string.IsNullOrEmpty(searchText)){
                root = root.Where(y => y.Name.Contains(searchText));
            }
            
            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = root
                .Include(y => y.Branding)
                .Include(y => y.ContactInformation)
                .Include(y => y.Locations)
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedItemsViewModel<Site>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        [HttpGet]
        //[Route("sites/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Site), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSites(Guid? id, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<Site>)_siteDbContext.Sites;
                                           
            if (id.HasValue)
            {
                root = root.Where(y => y.Id.Equals(id));
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = root
                .Include(y=>y.Branding)
                .Include(y=>y.ContactInformation)
                .Include(y => y.Locations)
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedItemsViewModel<Site>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        [HttpGet]
        [Route("withSiteId/{siteId:guid}/LocationId/{locationId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Location), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocaions(Guid siteId, Guid? locationId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            if (siteId == Guid.Empty)
            {
                return BadRequest();
            }
            var root = (IQueryable<Location>)_siteDbContext.Locations.Where(y=>y.SiteId.Equals(siteId))
                                                           .Include(y=>y.AdditionalLocationImages)
                                                           .Include(y=>y.Site);

            if (locationId.HasValue)
            {
                root = root.Where(y => y.Id.Equals(locationId));
            }


            var totalItems = await root
                .LongCountAsync();

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

        [HttpGet]
        [Route("withSiteId/{siteId:guid}/ServiceItems")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSeriviceItems(Guid siteId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0, [FromQuery] string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                return GetItemsByIds(siteId, ids);
            }

            var root = (IQueryable<ServiceItem>)_siteDbContext.ServiceItems.Where(y => y.SiteId.Equals(siteId));

            var totalItems = await root.LongCountAsync();

            var itemsOnPage = root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedItemsViewModel<ServiceItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        private IActionResult GetItemsByIds(Guid siteId, string ids)
        {
            var numIds = ids.Split(',')
                            .Select(id => (Ok: Guid.TryParse(id, out Guid x), Value: x));
            if (!numIds.All(nid => nid.Ok))
            {
                return BadRequest("ids value invalid. Must be comma-separated list of numbers");
            }

            var idsToSelect = numIds.Select(id => id.Value);
            var items = _siteDbContext.ServiceItems
                                      .Where(ci => ci.SiteId.Equals(siteId) && idsToSelect.Contains(ci.Id)).ToList();
            
            return Ok(items);

        }

        private List<Location> ChangeUriPlaceholder(List<Location> locations)
        {
            var baseUri = _settings.LocationPicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            foreach (var item in locations)
            {
                item.FillLocationUrl(baseUri, azureStorageEnabled: azureStorageEnabled);
                if (item.AdditionalLocationImages != null)
                {
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
