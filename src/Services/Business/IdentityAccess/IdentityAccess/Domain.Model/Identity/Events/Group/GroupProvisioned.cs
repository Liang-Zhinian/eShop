using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Events.Group
{
    public class GroupProvisioned : IDomainEvent
    {
        public GroupProvisioned(Guid tenantId, string name)
        {
            this.Name = name;
            this.TenantId = tenantId;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }


        public string Name { get; private set; }

        public Guid TenantId { get; private set; }
    }
}
