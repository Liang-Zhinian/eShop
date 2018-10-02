using System;
using CqrsFramework.Events;

namespace SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.Tenant
{
    public class TenantActivatedIntegrationEvent : IEvent
    {
        public TenantActivatedIntegrationEvent()
        {

        }

        public TenantActivatedIntegrationEvent(TenantId tenantId)
        {
            this.Id = Guid.Parse(tenantId.Id);
            this.TenantId = tenantId.Id;
            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string TenantId { get; private set; }
    }
}
