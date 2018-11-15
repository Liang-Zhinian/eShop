using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class ProductType: CatalogType
    {

        public Guid Id { get; set; }

        public string Type { get; set; }

        public Guid SiteId { get; set; }

        private ProductType()
        {
            GuidId = Guid.NewGuid();
        }

        public ProductType(Guid siteId, Guid productTypeId, string type, string description) : this()
        {
            SiteId = siteId;
            GuidId = productTypeId;
            Type = type;
            Description = description;
        }
    }
}