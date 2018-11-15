using System;

namespace SaaSEqt.eShop.WebMVC.ViewModels
{
    public class CatalogItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public Guid CatalogBrandId { get; set; }
        public string CatalogBrand { get; set; }
        public Guid CatalogTypeId { get; set; }
        public string CatalogType { get; set; }
        public Guid MerchantId { get; set; }
    }
}