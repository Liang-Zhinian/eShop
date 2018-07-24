
namespace SaaSEqt.IdentityAccess.Domain.Identity.Events.Group
{
    using System;
    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

    public class GroupGroupAdded : IDomainEvent
    {
        public GroupGroupAdded(TenantId tenantId, string groupName, string nestedGroupName)
        {
            this.GroupName = groupName;
            this.NestedGroupName = nestedGroupName;
            this.TenantId = tenantId.Id;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }


        public string GroupName { get; private set; }

        public string NestedGroupName { get; private set; }

        public string TenantId { get; private set; }
    }
}
