using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Events.Tenant
{
    public class TenantProvisioned : IDomainEvent
    {
        public TenantProvisioned(Guid tenantId, string name, string description, bool active)
        {
            this.TenantId = tenantId;
            Version = 1;
            TimeStamp = DateTimeOffset.Now;
            Name = name;
            Description = description;
            Active = active;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid TenantId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
    }
}
