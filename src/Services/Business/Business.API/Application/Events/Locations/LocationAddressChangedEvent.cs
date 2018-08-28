using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Locations
{
    public class LocationAddressChangedEvent : IntegrationEvent
    {
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
            string streetAddress,
            string city,
            string stateProvince,
            string postalCode,
            string countryCode)
        {
            this.SiteId = siteId;
            this.StreetAddress = streetAddress;
            this.City = city;
            this.StateProvince = stateProvince;
            this.PostalCode = postalCode;
            this.CountryCode = countryCode;
        }
    }
}
