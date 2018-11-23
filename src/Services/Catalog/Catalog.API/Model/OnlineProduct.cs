using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class OnlineProduct
    {
        public OnlineProduct()
        {
        }

        public Guid Id { get; set; }

        public string LongDescription { get; set; }

        /// The sales tax for this product
        public double TaxRate { get; set; }

        public double TaxAmount => OnlinePrice * TaxRate;

        /// ??? The tax
        public double TaxIncluded { get; set; }

        /// The online price
        public double OnlinePrice { get; set; }

        public bool SellOnline { get; set; }

        public Guid CatalogItemId { get; set; }
        public CatalogItem CatalogItem { get; set; }
    }
}
