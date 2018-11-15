using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class CatalogBrand
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public Guid MerchantId { get; set; }
    }
}
