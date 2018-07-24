using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Identity.Events.User
{
    public class UserEnablementChanged : IDomainEvent
    {
        public UserEnablementChanged(
                TenantId tenantId,
            Guid userId,
                String username,
                Enablement enablement)
        {
            this.Enablement = enablement;
            this.TenantId = tenantId.Id;
            this.UserId = userId;
            this.Username = username;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public Guid UserId { get; set; }

        public Enablement Enablement { get; private set; }

        public string TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
