﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.Resilience.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SaaSEqt.eShop.Web.Shopping.HttpAggregator.Config;
using SaaSEqt.eShop.Web.Shopping.HttpAggregator.Models;

namespace SaaSEqt.eShop.Web.Shopping.HttpAggregator.Services
{
    public class CatalogService : ICatalogService
    {

        private readonly IHttpClient _apiClient;
        private readonly ILogger<CatalogService> _logger;
        private readonly UrlsConfig _urls;

        public CatalogService(IHttpClient httpClient, ILogger<CatalogService> logger, IOptionsSnapshot<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<CatalogItem> GetCatalogItem(int id)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.CatalogOperations.GetItemById(id));
            var item = JsonConvert.DeserializeObject<CatalogItem>(data);
            return item;
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItems(IEnumerable<int> ids)
        {
            var data = await _apiClient.GetStringAsync(_urls.Catalog + UrlsConfig.CatalogOperations.GetItemsById(ids));
            var item = JsonConvert.DeserializeObject<CatalogItem[]>(data);
            return item;

        }
    }
}
