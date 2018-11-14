using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class CatalogType
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public Guid GuidId { get; set; }
        public Guid MerchantId { get; set; }
    }
}
