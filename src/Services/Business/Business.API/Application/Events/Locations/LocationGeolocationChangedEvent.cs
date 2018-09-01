using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Locations
{
    public class LocationGeolocationChangedEvent : IntegrationEvent
    {
        private Guid SiteId { get; set; }
        public Guid LocationId { get; set; }
        private double? Latitude { get; set; }
        private double? Longitude { get; set; }

        public LocationGeolocationChangedEvent(Guid siteId, Guid locationId, double? latitude, double? longitude)
        {
            this.SiteId = siteId;
            this.LocationId = locationId;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}