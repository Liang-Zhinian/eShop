using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Locations
{
    public class LocationGeolocationChangedEvent : IntegrationEvent
    {
        public Guid SiteId { get; set; }
        public Guid LocationId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public LocationGeolocationChangedEvent(Guid siteId, Guid locationId, double? latitude, double? longitude)
        {
            this.SiteId = siteId;
            this.LocationId = locationId;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}