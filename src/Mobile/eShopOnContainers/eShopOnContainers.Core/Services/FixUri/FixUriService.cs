using eShop.Core.Models.Basket;
using eShop.Core.Models.Catalog;
using eShop.Core.Models.Marketing;
using eShop.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using eShop.Core.Services.Settings;

namespace eShop.Core.Services.FixUri
{
    public class FixUriService : IFixUriService
    {
        private readonly ISettingsService _settingsService;

        private Regex IpRegex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

        public FixUriService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public void FixCatalogItemPictureUri(IEnumerable<CatalogItem> catalogItems)
        {
            if (catalogItems == null)
            {
                return;
            }

            try
            {
                if (!ViewModelLocator.UseMockService
                    && _settingsService.IdentityEndpointBase != GlobalSetting.DefaultEndpoint)
                {
                    foreach (var catalogItem in catalogItems)
                    {
                        MatchCollection serverResult = IpRegex.Matches(catalogItem.PictureUri);
                        MatchCollection localResult = IpRegex.Matches(_settingsService.IdentityEndpointBase);

                        if (serverResult.Count != -1 && localResult.Count != -1)
                        {
                            var serviceIp = serverResult[0].Value;
                            var localIp = localResult[0].Value;

                            catalogItem.PictureUri = catalogItem.PictureUri.Replace(serviceIp, localIp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void FixBasketItemPictureUri(IEnumerable<BasketItem> basketItems)
        {
            if (basketItems == null)
            {
                return;
            }

            try
            {
                if (!ViewModelLocator.UseMockService
                    && _settingsService.IdentityEndpointBase != GlobalSetting.DefaultEndpoint)
                {
                    foreach (var basketItem in basketItems)
                    {
                        MatchCollection serverResult = IpRegex.Matches(basketItem.PictureUrl);
                        MatchCollection localResult = IpRegex.Matches(_settingsService.IdentityEndpointBase);

                        if (serverResult.Count != -1 && localResult.Count != -1)
                        {
                            var serviceIp = serverResult[0].Value;
                            var localIp = localResult[0].Value;
                            basketItem.PictureUrl = basketItem.PictureUrl.Replace(serviceIp, localIp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void FixCampaignItemPictureUri(IEnumerable<CampaignItem> campaignItems)
        {
            if (campaignItems == null)
            {
                return;
            }

            try
            {
                if (!ViewModelLocator.UseMockService
                    && _settingsService.IdentityEndpointBase != GlobalSetting.DefaultEndpoint)
                {
                    foreach (var campaignItem in campaignItems)
                    {
                        MatchCollection serverResult = IpRegex.Matches(campaignItem.PictureUri);
                        MatchCollection localResult = IpRegex.Matches(_settingsService.IdentityEndpointBase);

                        if (serverResult.Count != -1 && localResult.Count != -1)
                        {
                            var serviceIp = serverResult[0].Value;
                            var localIp = localResult[0].Value;

                            campaignItem.PictureUri = campaignItem.PictureUri.Replace(serviceIp, localIp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
