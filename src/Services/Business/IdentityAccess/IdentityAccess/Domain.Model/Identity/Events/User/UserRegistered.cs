using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Events.User
{
    public class UserRegistered : IDomainEvent
    {
        public UserRegistered(
            Guid tenantId,
            Guid userId,
                String username,
                FullName name,
                EmailAddress emailAddress)
        {
            this.EmailAddress = emailAddress;
            this.Name = name;
            this.TenantId = tenantId;
            this.UserId = userId;
            this.Username = username;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public Guid UserId { get; set; }

        public EmailAddress EmailAddress { get; private set; }

        public FullName Name { get; private set; }

        public Guid TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
