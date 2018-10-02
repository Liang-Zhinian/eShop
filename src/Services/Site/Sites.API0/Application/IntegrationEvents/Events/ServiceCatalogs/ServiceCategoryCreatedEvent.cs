using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.ServiceCatalogs
{
    public class ServiceCategoryCreatedEvent : IntegrationEvent
    {
        public ServiceCategoryCreatedEvent()
        {
            
        }

        public Guid ServiceCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowOnlineScheduling { get; set; }
        public int ScheduleTypeValue { get; set; }
        public Guid SiteId { get; set; }
    }
}
