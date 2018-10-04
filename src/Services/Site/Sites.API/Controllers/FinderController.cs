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
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Services;
using SaaSEqt.eShop.Services.Sites.API.Model;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;
using Sites.API.Extensions;
using Sites.API.IntegrationEvents;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sites.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class FinderController : Controller
    {
        private readonly SitesContext _sitesContext;
        private readonly SitesSettings _settings;

        public FinderController(SitesContext context, IOptionsSnapshot<SitesSettings> settings, ISitesIntegrationEventService sitesIntegrationEventService)
        {
            _sitesContext = context ?? throw new ArgumentNullException(nameof(context));

            _settings = settings.Value;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Location>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<Location>)_sitesContext.Locations.Include(y => y.AdditionalLocationImages);

            if (!string.IsNullOrEmpty(searchText))
            {
                root = root.Where(y => y.Name.Contains(searchText));
            }

            IList<Location> locations = await root.ToListAsync();

            foreach (var item in locations)
            {
                item.Distance = DistanceHelper.Distance(latitude, longitude, item.Geolocation.Latitude, item.Geolocation.Longitude, 'K');
            }


            locations = locations
               .Where(y => y != null && y.Distance <= radius)
                .ToList();
            
            var totalItems = locations.LongCount();

            var itemsOnPage = locations
                .OrderBy(c => c.Distance)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<Location>(
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
