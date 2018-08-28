using System;
namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Staffs
{
    public class StaffEvent
    {
        public StaffEvent()
        {
        }

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PrimaryTelephone { get; set; }

        public string SecondaryTelephone { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }

        public string PostalCode { get; set; }

        public string StateProvince { get; set; }

        public string StreetAddress { get; set; }

        public bool IsMale { get; set; }

        public string Bio { get; set; }

        public string ImageUrl { get; set; }

        public bool CanLoginAllLocations { get; set; }

        public bool Enabled { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public Guid TenantId { get; set; }
    }
}
