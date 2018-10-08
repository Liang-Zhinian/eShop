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
    public class BusinessLocationService : IBusinessLocationService
    {

        private readonly IHttpClient _apiClient;
        private readonly ILogger<BusinessLocationService> _logger;
        private readonly UrlsConfig _urls;

        public BusinessLocationService(IHttpClient httpClient, ILogger<BusinessLocationService> logger, IOptionsSnapshot<UrlsConfig> config)
        {
            _apiClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<PaginatedItemsViewModel<Location>> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText, int pageSize = 10, int pageIndex = 0)
        {
            var data = await _apiClient.GetStringAsync(_urls.Business + UrlsConfig.BusinessLocationOperations.GetBusinessLocationsWithinRadius(latitude, longitude, radius, searchText, pageSize, pageIndex));
            var item = JsonConvert.DeserializeObject<PaginatedItemsViewModel<Location>>(data);
            return item;
        }

        public async Task<Location> GetBusinessLocation(Guid siteId, Guid locationId)
        {
            var data = await _apiClient.GetStringAsync(_urls.Business + UrlsConfig.BusinessLocationOperations.GetBusinessLocation(siteId, locationId));
            var item = JsonConvert.DeserializeObject<Location>(data);
            return item;
        }
    }
}
