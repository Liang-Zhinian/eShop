
namespace SaaSEqt.IdentityAccess.Domain.Model.Access.Events
{
    using System;
    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Model.Access.Entities;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

    public class GroupAssignedToRole : IDomainEvent
    {
        public GroupAssignedToRole(Guid tenantId, string roleName, string groupName)
        {
            this.GroupName = groupName;
            this.RoleName = roleName;
            this.TenantId = tenantId;
            Version = 1;
            TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public string GroupName { get; private set; }

        public string RoleName { get; private set; }

        public Guid TenantId { get; private set; }
    }
}
