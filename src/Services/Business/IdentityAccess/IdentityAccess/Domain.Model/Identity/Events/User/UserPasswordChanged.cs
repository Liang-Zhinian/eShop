using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Events.User
{
    public class UserPasswordChanged : IDomainEvent
    {
        public UserPasswordChanged(
            Guid tenantId,
            Guid userId,
                String username)
        {
            this.TenantId = tenantId;
            this.UserId = userId;
            this.Username = username;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public Guid TenantId { get; private set; }

        public Guid UserId { get; set; }

        public string Username { get; private set; }
    }
}
