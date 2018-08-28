using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsFramework.Events;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.Tenant
{
    public class TenantDeactivatedIntegrationEvent : IEvent
    {
        public TenantDeactivatedIntegrationEvent()
        {

        }

        public TenantDeactivatedIntegrationEvent(TenantId tenantId)
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
