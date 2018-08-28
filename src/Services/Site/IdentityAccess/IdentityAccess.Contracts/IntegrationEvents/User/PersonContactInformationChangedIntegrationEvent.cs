
namespace SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.User
{
    using System;
    using CqrsFramework.Events;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

    public class PersonContactInformationChangedIntegrationEvent : IEvent
    {
        public PersonContactInformationChangedIntegrationEvent(
            TenantId tenantId,
                Guid userId,
                String username,
                ContactInformation contactInformation)
        {
            this.ContactInformation = contactInformation;
            this.TenantId = tenantId.Id;
            this.Username = username;

            this.Id = userId;
            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public ContactInformation ContactInformation { get; set; }

        public string TenantId { get; set; }

        public string Username { get; set; }
    }
}
