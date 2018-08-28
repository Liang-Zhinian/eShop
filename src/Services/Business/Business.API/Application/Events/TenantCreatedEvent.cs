using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events
{
    public class TenantCreatedEvent : IntegrationEvent
    {
        public TenantCreatedEvent(Guid tenantId, string name, string description)
        {
            TenantId = tenantId;
            Name = name;
            Description = description;
            Version = 1;
            TimeStamp = DateTimeOffset.Now;

        }
        public Guid TenantId { get; set; }

        public int Version { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string PrimaryTelephone { get; set; }

        public string SecondaryTelephone { get; set; }

        public string Street { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string LogoURL { get; set; }

        public string PageColor1 { get; set; }

        public string PageColor2 { get; set; }

        public string PageColor3 { get; set; }

        public string PageColor4 { get; set; }
    }
}
