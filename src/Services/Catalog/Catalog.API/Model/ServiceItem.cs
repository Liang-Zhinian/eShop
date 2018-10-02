using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class ServiceItem
    {
        private ServiceItem()
        {
            Id = Guid.NewGuid();
        }

        public ServiceItem(Guid siteId, Guid serviceItemId, string name, string description, int defaultTimeLength, double price, Guid serviceCategoryId, string industryStandardCategoryName, string industryStandardSubcategoryName)
        {
            SiteId = siteId;
            Id = serviceItemId;
            Name = name;
            Description = description;
            DefaultTimeLength = defaultTimeLength;
            ServiceCategoryId = serviceCategoryId;
            Price = price;
            IndustryStandardCategoryName = industryStandardCategoryName;
            IndustryStandardSubcategoryName = industryStandardSubcategoryName;

        }

        #region public properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DefaultTimeLength { get; set; }

        public Guid ServiceCategoryId { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; }

        public string IndustryStandardCategoryName { get; set; }

        public string IndustryStandardSubcategoryName { get; set; }

        public double Price { get; set; }

        public bool AllowOnlineScheduling { get; set; } // or Show in Consumer Mode?

        public double TaxRate { get; set; }

        public double TaxAmount => Price * TaxRate;

        public Guid SiteId { get; set; }

        #endregion

    }
}
