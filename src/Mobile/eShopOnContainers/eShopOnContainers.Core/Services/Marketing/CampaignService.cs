using eShop.Core.Extensions;
using eShop.Core.Helpers;
using eShop.Core.Models.Marketing;
using eShop.Core.Services.FixUri;
using eShop.Core.Services.RequestProvider;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace eShop.Core.Services.Marketing
{
    public class CampaignService : ICampaignService
    {
        private readonly IRequestProvider _requestProvider;
        private readonly IFixUriService _fixUriService;

        private const string ApiUrlBase = "api/v1/m/campaigns";

        public CampaignService(IRequestProvider requestProvider, IFixUriService fixUriService)
        {
            _requestProvider = requestProvider;
            _fixUriService = fixUriService;
        }

        public async Task<ObservableCollection<CampaignItem>> GetAllCampaignsAsync(string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayMarketingEndpoint, $"{ApiUrlBase}/user");

            CampaignRoot campaign = await _requestProvider.GetAsync<CampaignRoot>(uri, token);

            if (campaign?.Data != null)
            {
                _fixUriService.FixCampaignItemPictureUri(campaign?.Data);
                return campaign?.Data.ToObservableCollection();
            }

            return new ObservableCollection<CampaignItem>();
        }

        public async Task<CampaignItem> GetCampaignByIdAsync(int campaignId, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayMarketingEndpoint, $"{ApiUrlBase}/{campaignId}");

            return await _requestProvider.GetAsync<CampaignItem>(uri, token);
        }
    }
}