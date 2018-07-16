namespace SaaSEqt.eShop.Services.Locations.API.IntegrationEvents.Events
{
    using Locations.API.Model;
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
    using System.Collections.Generic;

    public class UserLocationUpdatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public List<UserLocationDetails> LocationList { get; set; }

        public UserLocationUpdatedIntegrationEvent(string userId, List<UserLocationDetails> locationList)
        {
            UserId = userId;
            LocationList = locationList;
        }
    }
}
