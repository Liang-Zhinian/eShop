using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class CatalogBrand
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public Guid GuidId { get; set; }
        public Guid MerchantId { get; set; }
    }
}
