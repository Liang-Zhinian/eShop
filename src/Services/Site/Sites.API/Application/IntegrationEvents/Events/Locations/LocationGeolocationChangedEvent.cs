using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations
{
    public class LocationGeolocationChangedEvent : IntegrationEvent
    {
        public Guid SiteId { get; set; }
        public Guid LocationId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public LocationGeolocationChangedEvent()
        {

        }
    }
}