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
    public class SchedulableCatalogService : ISchedulableCatalogService
    {

        private readonly IHttpClient _apiClient;
        private readonly ILogger<CatalogService> _logger;
        private readonly UrlsConfig _urls;

        public SchedulableCatalogService(IHttpClient httpClient, ILogger<CatalogService> logger, IOptionsSnapshot<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<SchedulableCatalogItem> GetCatalogItem(Guid id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetItemById(id));
            var item = JsonConvert.DeserializeObject<SchedulableCatalogItem>(data);
            return item;
        }

        public async Task<IEnumerable<SchedulableCatalogItem>> GetCatalogItems(IEnumerable<Guid> ids)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetItemsById(ids));
            var item = JsonConvert.DeserializeObject<SchedulableCatalogItem[]>(data);
            return item;

        }

        public async Task<SchedulableCatalogType> GetCatalog(Guid id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetCatalogById(id));
            var item = JsonConvert.DeserializeObject<SchedulableCatalogType>(data);
            return item;
        }

        public async Task<IEnumerable<SchedulableCatalogType>> GetCatalogs(IEnumerable<Guid> ids)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.SchedulableCatalogOperations.GetCatalogsById(ids));
            var item = JsonConvert.DeserializeObject<SchedulableCatalogType[]>(data);
            return item;
        }
    }
}
