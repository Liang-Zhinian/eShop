using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Locations
{
    public class LocationGeolocationChangedEvent : IntegrationEvent
    {
        private Guid SiteId { get; set; }
        private double? Latitude { get; set; }
        private double? Longitude { get; set; }

        public LocationGeolocationChangedEvent(Guid siteId, double? latitude, double? longitude)
        {
            this.SiteId = siteId;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}