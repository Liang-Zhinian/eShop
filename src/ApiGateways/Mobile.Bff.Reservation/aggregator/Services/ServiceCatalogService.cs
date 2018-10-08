using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.Resilience.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Config;
using SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Models;

namespace SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Services
{
    public class ServiceCatalogService : IServiceCatalogService
    {

        private readonly IHttpClient _apiClient;
        private readonly ILogger<ServiceCatalogService> _logger;
        private readonly UrlsConfig _urls;

        public ServiceCatalogService(IHttpClient httpClient, ILogger<ServiceCatalogService> logger, IOptionsSnapshot<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<ServiceItem> GetCatalogItem(Guid id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.ServiceCatalogOperations.GetItemById(id));
            var item = JsonConvert.DeserializeObject<ServiceItem>(data);
            return item;
        }

        public async Task<PaginatedItemsViewModel<ServiceItem>> GetSeriviceItemsWithinRadius(double latitude, double longitude, double radius, string searchText, int pageSize = 10, int pageIndex = 0)
        {
            var locationsData = await _apiClient.GetStringAsync(_urls.Business + UrlsConfig.BusinessLocationOperations.GetBusinessLocationsWithinRadius(latitude, longitude, radius, "", pageSize, pageIndex));
            var locations = JsonConvert.DeserializeObject<PaginatedItemsViewModel<Location>>(locationsData);

            string siteIds = string.Join(",", locations.Data.Select(y => y.SiteId));
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.ServiceCatalogOperations.GetItemsBySiteIdsAndSearchText(locations.Data.Select(y => y.SiteId), searchText));
            var items = JsonConvert.DeserializeObject<PaginatedItemsViewModel<ServiceItem>>(data);

            return items;

        }

        public async Task<ServiceCategory> GetCatalog(Guid id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.ServiceCatalogOperations.GetCatalogById(id));
            var item = JsonConvert.DeserializeObject<ServiceCategory>(data);
            return item;
        }

        public async Task<IEnumerable<ServiceCategory>> GetCatalogs(IEnumerable<Guid> ids)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.ServiceCatalogOperations.GetCatalogsById(ids));
            var item = JsonConvert.DeserializeObject<ServiceCategory[]>(data);
            return item;
        }
    }
}
