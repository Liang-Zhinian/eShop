using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Identity.Events.Tenant
{
    public class TenantProvisioned : IDomainEvent
    {
        public TenantProvisioned(TenantId tenantId, string name, string description, bool active)
        {
            this.TenantId = tenantId.Id;
            Version = 1;
            TimeStamp = DateTimeOffset.Now;
            Name = name;
            Description = description;
            Active = active;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string TenantId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
    }
}
