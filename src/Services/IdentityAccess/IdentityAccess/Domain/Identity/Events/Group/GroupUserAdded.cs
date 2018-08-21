using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Identity.Events.Group
{
    public class GroupUserAdded : IDomainEvent
    {
        public GroupUserAdded(Guid tenantId, string groupName, string username)
        {
            this.GroupName = groupName;
            this.TenantId = tenantId;
            this.Username = username;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }


        public string GroupName { get; private set; }

        public Guid TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
