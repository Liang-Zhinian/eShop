using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Staffs
{
    public class StaffCreatedEvent : IntegrationEvent
    {
        public Guid StaffId { get; set; }
        public Guid TenantId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmailAddress { get; set; }
        public string PrimaryTelephone { get; set; }
        public string SecondaryTelephone { get; set; }
        public string AddressStreetAddress { get; set; }
        public string AddressCity { get; set; }
        public string AddressStateProvince { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCountryCode { get; set; }
        public bool IsMale { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }
        public bool CanLoginAllLocations { get; set; } = false;
    }
}