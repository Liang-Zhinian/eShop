using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Locations
{
    public class LocationCreatedEvent : IntegrationEvent
    {
        protected LocationCreatedEvent()
        {

        }

        public LocationCreatedEvent(
            Guid siteId,
            Guid locationId,
            string name,
            string description)
        {
            this.SiteId = siteId;
            this.LocationId = locationId;
            this.Name = name;
            this.Description = description;
            //this.ContactName = contactName;
            //this.EmailAddress = emailAddress;
            //this.PrimaryTelephone = primaryTelephone;
            //this.SecondaryTelephone = secondaryTelephone;
        }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public string ContactName { get; set; }

        //public string EmailAddress { get; set; }

        //public string PrimaryTelephone { get; set; }

        //public string SecondaryTelephone { get; set; }

        public bool Active { get; set; }
    }
}
