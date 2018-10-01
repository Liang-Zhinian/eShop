using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations
{
    public class LocationCreatedEvent : IntegrationEvent
    {
        protected LocationCreatedEvent()
        {

        }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string EmailAddress { get; set; }

        public string PrimaryTelephone { get; set; }

        public string SecondaryTelephone { get; set; }

        public bool Active { get; set; }
    }
}
