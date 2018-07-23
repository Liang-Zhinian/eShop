using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.Resilience.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Config;
using SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Models;

namespace SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Services
{
    public class ServiceCatalogService : IServiceCatalogService
    {

        private readonly IHttpClient _apiClient;
        private readonly ILogger<CatalogService> _logger;
        private readonly UrlsConfig _urls;

        public ServiceCatalogService(IHttpClient httpClient, ILogger<CatalogService> logger, IOptionsSnapshot<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<ServiceItem> GetCatalogItem(Guid id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetItemById(id));
            var item = JsonConvert.DeserializeObject<ServiceItem>(data);
            return item;
        }

        public async Task<IEnumerable<ServiceItem>> GetCatalogItems(IEnumerable<Guid> ids)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetItemsById(ids));
            var item = JsonConvert.DeserializeObject<ServiceItem[]>(data);
            return item;

        }

        public async Task<ServiceCategory> GetCatalog(Guid id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetCatalogById(id));
            var item = JsonConvert.DeserializeObject<ServiceCategory>(data);
            return item;
        }

        public async Task<IEnumerable<ServiceCategory>> GetCatalogs(IEnumerable<Guid> ids)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetCatalogsById(ids));
            var item = JsonConvert.DeserializeObject<ServiceCategory[]>(data);
            return item;
        }
    }
}
