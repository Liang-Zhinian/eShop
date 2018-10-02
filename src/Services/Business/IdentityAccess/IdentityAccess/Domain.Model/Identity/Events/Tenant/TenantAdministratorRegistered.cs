
namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Events.Tenant
{
    using System;
    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

    public class TenantAdministratorRegistered : IDomainEvent
    {
        public TenantAdministratorRegistered(
            Guid tenantId,
            string name,
            FullName administorName,
            EmailAddress emailAddress,
            string username,
            string temporaryPassword)
        {
            this.AdministorName = administorName;
            this.EmailAddress = emailAddress;
            this.Name = username;
            this.TemporaryPassword = temporaryPassword;
            this.TenantId = tenantId;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public Guid UserId { get; private set; }

        public FullName AdministorName { get; private set; }

        public EmailAddress EmailAddress { get; private set; }

        public string Name { get; private set; }

        public string TemporaryPassword { get; private set; }

        public Guid TenantId { get; private set; }
    }
}
