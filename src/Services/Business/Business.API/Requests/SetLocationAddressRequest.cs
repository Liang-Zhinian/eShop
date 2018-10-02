using System;
namespace Business.API.Requests.Locations
{
    public class SetLocationAddressRequest
    {

        public SetLocationAddressRequest()
        {
        }

        public SetLocationAddressRequest(Guid siteId, Guid id, string street, string city, string stateProvince, string zipCode, string country)
        {
            SiteId = siteId;
            Id = id;
            StreetAddress = street;
            City = city;
            StateProvince = stateProvince;
            PostalCode = zipCode;
            CountryCode = country;
        }

        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }

        public string PostalCode { get; set; }

        public string StateProvince { get; set; }

        public string StreetAddress { get; set; }

        //public string StreetAddress2 { get; set; }
    }
}
