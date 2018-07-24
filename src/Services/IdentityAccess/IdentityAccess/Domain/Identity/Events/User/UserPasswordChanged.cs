using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Identity.Events.User
{
    public class UserPasswordChanged : IDomainEvent
    {
        public UserPasswordChanged(
                TenantId tenantId,
            Guid userId,
                String username)
        {
            this.TenantId = tenantId.Id;
            this.UserId = userId;
            this.Username = username;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public string TenantId { get; private set; }

        public Guid UserId { get; set; }

        public string Username { get; private set; }
    }
}
