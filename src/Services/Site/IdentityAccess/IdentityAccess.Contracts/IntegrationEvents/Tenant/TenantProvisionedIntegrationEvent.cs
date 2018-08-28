using System;
using CqrsFramework.Events;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.Tenant
{
    public class TenantProvisionedIntegrationEvent : IEvent
    {
        public TenantProvisionedIntegrationEvent()
        {

        }

        public TenantProvisionedIntegrationEvent(Guid tenantId, string name, string description, bool active)
        {
            this.Id = tenantId;
            this.TenantId = tenantId;
            Version = 1;
            TimeStamp = DateTimeOffset.Now;
            Name = name;
            Description = description;
            Active = active;
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
