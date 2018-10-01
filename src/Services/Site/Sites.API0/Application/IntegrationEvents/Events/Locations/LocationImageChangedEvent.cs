using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations
{
    public class LocationImageChangedEvent: IntegrationEvent
    {
        public LocationImageChangedEvent(Guid siteId, Guid locationId, string fileName)
        {
            this.SiteId = siteId;
            this.LocationId = locationId;
            this.FileName = fileName;
        }

        public string FileName { get; set; }

        public Guid SiteId { get; set; }

        public Guid LocationId { get; set; }
    }
}
