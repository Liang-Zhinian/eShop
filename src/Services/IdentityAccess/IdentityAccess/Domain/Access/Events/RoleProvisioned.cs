using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Access.Entities;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Access.Events
{
    public class RoleProvisioned : IDomainEvent
    {
        public RoleProvisioned(TenantId tenantId, string name)
        {
            this.Name = name;
            this.TenantId = tenantId.Id;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public string Name { get; private set; }

        public string TenantId { get; private set; }
    }
}
