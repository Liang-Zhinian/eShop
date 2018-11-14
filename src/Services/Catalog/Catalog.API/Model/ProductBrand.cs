using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class ProductBrand
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public Guid SiteId { get; set; }
    }
}
