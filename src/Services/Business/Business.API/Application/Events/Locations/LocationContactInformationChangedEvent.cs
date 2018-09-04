using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Locations
{
	public class LocationContactInformationChangedEvent: IntegrationEvent
    {
        public Guid SiteId { get; set; }

        public Guid LocationId { get; set; }

        public string ContactName { get; set; }

        public string EmailAddress { get; set; }

        public string PrimaryTelephone { get; set; }

        public string SecondaryTelephone { get; set; }

        private LocationContactInformationChangedEvent()
        {
        }

        public LocationContactInformationChangedEvent(Guid siteId, Guid locationId, string contactName, string primaryTelephone, string secondaryTelephone, string emailAddress)
        {
            SiteId = siteId;
            LocationId = locationId;
            ContactName = contactName;
            PrimaryTelephone = primaryTelephone;
            SecondaryTelephone = secondaryTelephone;
            EmailAddress = emailAddress;
        }
    }
}
