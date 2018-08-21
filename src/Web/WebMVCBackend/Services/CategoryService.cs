using Microsoft.AspNetCore.Mvc.Rendering;
using SaaSEqt.eShop.BuildingBlocks.Resilience.Http;
using SaaSEqt.eShop.WebMVCBackend.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMVCBackend.Infrastructure;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace SaaSEqt.eShop.WebMVCBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _apiClient;
        private readonly ILogger<CategoryService> _logger;
        private readonly IHttpContextAccessor _httpContextAccesor;

        private readonly string _remoteServiceBaseUrl;

        public CategoryService(IOptionsSnapshot<AppSettings> settings, 
                               IHttpContextAccessor httpContextAccesor, 
                               IHttpClient httpClient, 
                               ILogger<CategoryService> logger)
        {
            _settings = settings;
            _httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
            _logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.PurchaseUrl}/api/v1/sc/catalog/";
        }

        public async Task<IEnumerable<ServiceCategory>> GetCategories(int page, int take, Guid siteId)
        {
            var getTypesUri = API.Catalog.GetAllCategories(_remoteServiceBaseUrl, page, take, siteId);

            var dataString = await _apiClient.GetStringAsync(getTypesUri);

            IList<ServiceCategory> items = new List<ServiceCategory>();
            //items.Add(new SelectListItem() { Value = null, Text = "All", Selected = true });

            var categories = JArray.Parse(dataString);
            foreach (var category in categories.Children<JObject>())
            {
                items.Add(new ServiceCategory
                {
                    Id = category.Value<Guid>("id"),
                    Name = category.Value<string>("name")
                    //Description = category.Value<string>("description"),
                    //AllowOnlineScheduling = category.Value<bool>("allowOnlineScheduling"),
                    //ScheduleTypeId = category.Value<int>("scheduleTypeId"),
                    //SiteId = category.Value<Guid>("siteId")
                });
            }
            return items;
        }

        public async Task AddCategory(Guid siteId, string name, string description, bool allowOnlineScheduling, int scheduleType)
        {
            var token = await GetUserTokenAsync();
            var updateBasketUri = API.Catalog.AddCategory(_remoteServiceBaseUrl);

            var response = await _apiClient.PostAsync(updateBasketUri, new
            {
                SiteId = siteId,
                Name = name,
                Description = description,
                AllowOnlineScheduling = allowOnlineScheduling,
                ScheduleTypeId = scheduleType
            }, token);

        }

        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;
            return await context.GetTokenAsync("access_token");
        }
    }
}
