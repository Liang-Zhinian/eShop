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
using SaaSEqt.eShop.Services.Sites.API.Model.Security;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sites.API.Controllers
{
    /*
FinderService.AddOrUpdateFinderUsers
FinderService.FinderCheckoutShoppingCart
FinderService.GetBusinessLocationsWithinRadius
FinderService.GetClassesWithinRadius
FinderService.GetFinderUser
FinderService.GetSessionTypesWithinRadius
FinderService.SendFinderUserNewPassword
    */
    [Route("api/v1/[controller]")]
    public class FinderController : Controller
    {
        private readonly SitesDbContext _siteDbContext;
        private readonly IHostingEnvironment _env;
        private readonly BusinessSettings _settings;

        public FinderController(IHostingEnvironment env, 
                                   IOptionsSnapshot<BusinessSettings> settings, 
                                   SitesDbContext siteDbContext)
        {
            _env = env;
            _settings = settings.Value;
            _siteDbContext = siteDbContext;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Location>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Location>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = await _siteDbContext.Locations
                                           .Include(y => y.Site)
                                           .Include(y => y.AdditionalLocationImages)
                                           .ToListAsync();
            
            IList<Location> list = new List<Location>();
            foreach (var item in root)
            {
                if (DistanceHelper.Distance(latitude, longitude, item.Geolocation.Latitude, item.Geolocation.Longitude, 'K') <= radius)
                {
                    list.Add(item);
                }
            }

            var totalItems = list.LongCount();

            var itemsOnPage = list
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
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSeriviceItemsWithinRadius(double latitude, double longitude, double radius, string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var allLocations = await _siteDbContext.Locations.ToListAsync();
            IList<Location> locations = new List<Location>();
            foreach (var item in allLocations)
            {
                if (DistanceHelper.Distance(latitude, longitude, item.Geolocation.Latitude, item.Geolocation.Longitude, 'K') <= radius)
                {
                    locations.Add(item);
                }
            }

             IList<Guid> siteIds = (from loc in locations
                                    select loc.SiteId).ToList();

            var root = (IQueryable<ServiceItem>)_siteDbContext.ServiceItems.Where(y=>siteIds.Contains(y.SiteId));

            var num = await root.LongCountAsync();

            if (!string.IsNullOrEmpty(searchText))
                root = root.Where(y => y.Name.Contains(searchText));


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
