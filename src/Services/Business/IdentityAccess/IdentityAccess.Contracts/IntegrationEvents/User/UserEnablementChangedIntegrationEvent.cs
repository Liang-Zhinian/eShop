using System;
using CqrsFramework.Events;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

namespace SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.User
{
    public class UserEnablementChangedIntegrationEvent : IEvent
    {
        public UserEnablementChangedIntegrationEvent(
            TenantId tenantId,
                Guid userId,
                String username,
                Enablement enablement)
        {
            this.Enablement = enablement;
            this.TenantId = tenantId.Id;
            this.Username = username;

            this.Id = userId;
            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public Enablement Enablement { get; set; }

        public string TenantId { get; set; }

        public string Username { get; set; }
    }
}
