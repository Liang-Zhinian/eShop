
namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using SaaSEqt.Common.Domain.Model;

    [NotMapped]
    public class InvitationDescriptor
    {
        public InvitationDescriptor(Guid tenantId, string invitationId, string description, DateTime startingOn, DateTime until)
        {
            this.Description = description;
            this.InvitationId = invitationId;
            this.StartingOn = startingOn;
            this.TenantId = tenantId;
            this.Until = until;
        }

        public string Description { get; private set; }

        public string InvitationId { get; private set; }

        public DateTime StartingOn;

        public Guid TenantId { get; private set; }

        public DateTime Until { get; private set; }
    }
}
