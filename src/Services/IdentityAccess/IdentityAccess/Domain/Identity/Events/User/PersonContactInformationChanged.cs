
namespace SaaSEqt.IdentityAccess.Domain.Identity.Events.User
{
    using System;
    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

    public class PersonContactInformationChanged : IDomainEvent
    {
        public PersonContactInformationChanged(
            Guid tenantId,
            Guid userId,
                String username,
                ContactInformation contactInformation)
        {
            this.ContactInformation = contactInformation;
            this.TenantId = tenantId;
            this.UserId = userId;
            this.Username = username;

            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public ContactInformation ContactInformation { get; private set; }

        public Guid UserId { get; set; }

        public Guid TenantId { get; private set; }

        public string Username { get; private set; }
    }
}
