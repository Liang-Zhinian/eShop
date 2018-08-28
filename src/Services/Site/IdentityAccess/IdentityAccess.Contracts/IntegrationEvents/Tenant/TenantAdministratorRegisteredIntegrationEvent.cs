
namespace SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.Tenant
{
    using System;
    using CqrsFramework.Events;

    public class TenantAdministratorRegisteredIntegrationEvent : IEvent
    {
        public TenantAdministratorRegisteredIntegrationEvent()
        {

        }

        public TenantAdministratorRegisteredIntegrationEvent(
            TenantId tenantId,
            string name,
            FullName administorName,
            EmailAddress emailAddress,
            string username,
            string temporaryPassword)
        {
            this.Id = Guid.Parse(tenantId.Id);
            this.AdministorName = administorName;
            this.Name = username;
            this.TemporaryPassword = temporaryPassword;
            this.TenantId = tenantId.Id;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public FullName AdministorName { get; set; }

        public string Name { get; set; }

        public string TemporaryPassword { get; set; }
        public string TenantId { get; private set; }

    }
}
