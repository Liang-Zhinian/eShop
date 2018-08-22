
namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Events.Group
{
    using System;
    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

    public class GroupGroupAdded : IDomainEvent
    {
        public GroupGroupAdded(Guid tenantId, string groupName, string nestedGroupName)
        {
            this.GroupName = groupName;
            this.NestedGroupName = nestedGroupName;
            this.TenantId = tenantId;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }


        public string GroupName { get; private set; }

        public string NestedGroupName { get; private set; }

        public Guid TenantId { get; private set; }
    }
}
