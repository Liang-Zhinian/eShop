using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class CatalogType
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public Guid MerchantId { get; set; }
    }
}
