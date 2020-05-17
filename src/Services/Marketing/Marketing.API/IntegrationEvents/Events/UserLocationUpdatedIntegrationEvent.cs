namespace Eva.eShop.Services.Marketing.API.IntegrationEvents.Events
{
    using Marketing.API.Model;
    using Eva.BuildingBlocks.EventBus.Events;
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
