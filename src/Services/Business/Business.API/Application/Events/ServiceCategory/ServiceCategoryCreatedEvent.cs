using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.ServiceCategory
{

    public class ServiceCategoryCreatedEvent : IntegrationEvent
    {
        public ServiceCategoryCreatedEvent(Guid siteId,
                                           Guid serviceCategoryId,
                                           string name,
                                           string description,
                                           bool allowOnlineScheduling,
                                           int scheduleTypeValue)
        {
            ServiceCategoryId = serviceCategoryId;
            Name = name;
            Description = description;
            AllowOnlineScheduling = allowOnlineScheduling;
            ScheduleTypeValue = scheduleTypeValue;
            SiteId = siteId;
        }

        public Guid ServiceCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowOnlineScheduling { get; set; }
        public int ScheduleTypeValue { get; set; }
        public Guid SiteId { get; set; }
    }
}
