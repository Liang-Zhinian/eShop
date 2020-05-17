using System.Collections.Generic;
using eShop.Core.Models.Basket;
using eShop.Core.Models.Catalog;
using eShop.Core.Models.Marketing;

namespace eShop.Core.Services.FixUri
{
    public interface IFixUriService
    {
        void FixCatalogItemPictureUri(IEnumerable<CatalogItem> catalogItems);
        void FixBasketItemPictureUri(IEnumerable<BasketItem> basketItems);
        void FixCampaignItemPictureUri(IEnumerable<CampaignItem> campaignItems);
    }
}
