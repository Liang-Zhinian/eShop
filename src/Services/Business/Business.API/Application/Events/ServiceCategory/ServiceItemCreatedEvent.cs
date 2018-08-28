using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.ServiceCategory
{
    public class ServiceItemCreatedEvent : IntegrationEvent
    {
        public Guid ServiceItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool AllowOnlineScheduling { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string IndustryStandardCategoryName { get; set; }
        public string IndustryStandardSubcategoryName { get; set; }
        public Guid SiteId { get; set; }
        public int DefaultTimeLength { get; set; }

        public ServiceItemCreatedEvent(Guid siteId,
                                       Guid serviceItemId,
                                       string name,
                                       string description,
                                       int defaultTimeLength,
                                       double price,
                                       bool allowOnlineScheduling,
                                       Guid serviceCategoryId,
                                       string industryStandardCategoryName,
                                       string industryStandardSubcategoryName)
        {
            ServiceItemId = serviceItemId;
            Name = name;
            Description = description;
            DefaultTimeLength = defaultTimeLength;
            Price = price;
            AllowOnlineScheduling = allowOnlineScheduling;
            IndustryStandardCategoryName = industryStandardCategoryName;
            IndustryStandardSubcategoryName = industryStandardSubcategoryName;
            ServiceCategoryId = serviceCategoryId;
            SiteId = siteId;
        }
    }
}
