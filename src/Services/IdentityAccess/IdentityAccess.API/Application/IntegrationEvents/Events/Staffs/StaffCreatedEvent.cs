using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.IdentityAccess.API.Application.IntegrationEvents.Events.Staffs
{
    public class StaffCreatedEvent : IntegrationEvent
    {
        public StaffCreatedEvent(Guid id,
                                 Guid siteId,
                                 Guid tenantId, String username, String password, String firstName,
            String lastName, bool enabled, DateTime startDate, DateTime endDate, String emailAddress, String primaryTelephone,
            String secondaryTelephone, String addressStreetAddress, String addressCity, String addressStateProvince,
            String addressPostalCode, String addressCountryCode
                                )
        {
            Id = id;
            SiteId = siteId;

            this.TenantId = tenantId;
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Enabled = enabled;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.EmailAddress = emailAddress;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
            this.AddressStreetAddress = addressStreetAddress;
            this.AddressCity = addressCity;
            this.AddressStateProvince = addressStateProvince;
            this.AddressPostalCode = addressPostalCode;
            this.AddressCountryCode = addressCountryCode;
        }

        public Guid Id { get; set; }
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

        public Guid SiteId { get; set; }
        public bool IsMale { get; set; }

        public string Bio { get; set; }

        public string ImageUrl { get; set; }

        public bool CanLoginAllLocations { get; set; } = false;
    }
}