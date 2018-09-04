using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations
{
    public class LocationAddressChangedEvent : IntegrationEvent
    {

        public Guid LocationId { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }

        public string PostalCode { get; set; }

        public string StateProvince { get; set; }

        public string StreetAddress { get; set; }

        public Guid SiteId { get; set; }

        protected LocationAddressChangedEvent()
        {

        }

        public LocationAddressChangedEvent(
            Guid siteId,
            Guid locationId,
            string streetAddress,
            string city,
            string stateProvince,
            string postalCode,
            string countryCode)
        {
            this.SiteId = siteId;
            this.LocationId = locationId;
            this.StreetAddress = streetAddress;
            this.City = city;
            this.StateProvince = stateProvince;
            this.PostalCode = postalCode;
            this.CountryCode = countryCode;
        }
    }
}
