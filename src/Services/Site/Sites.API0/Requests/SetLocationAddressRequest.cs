using System;
namespace Business.API.Requests.Locations
{
    public class SetLocationAddressRequest
    {
        public SetLocationAddressRequest()
        {
        }

        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }

        public string PostalCode { get; set; }

        public string StateProvince { get; set; }

        public string StreetAddress { get; set; }

        public string StreetAddress2 { get; set; }
    }
}
