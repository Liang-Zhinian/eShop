
namespace SaaSEqt.IdentityAccess.Domain.Identity.Entities
{
	using System;
	using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel.DataAnnotations.Schema;

	using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Identity.Events.Tenant;
    using SaaSEqt.IdentityAccess.Domain.Identity.Events.Group;
    using SaaSEqt.IdentityAccess.Domain.Access.Events;
    using SaaSEqt.IdentityAccess.Domain.Access.Entities;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// An entity representing a tenant in a multi-tenant
    /// "Identity and Access" Bounded Context.
    /// </summary>
    /// <remarks>
    /// <see cref="Person"/>, <see cref="User"/>, <see cref="Group"/>,
    /// and <see cref="Role"/> entities are each bound to a single tenant.
    /// </remarks>

    public partial class Tenant : EntityWithCompositeId
    {
        #region [ Fields and Constructor Overloads ]

        private readonly ISet<RegistrationInvitation> registrationInvitations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class.
        /// </summary>
        /// <param name="tenantId">
        /// Initial value of the <see cref="TenantId"/> property.
        /// </param>
        /// <param name="name">
        /// Initial value of the <see cref="Name"/> property.
        /// </param>
        /// <param name="description">
        /// Initial value of the <see cref="Description"/> property.
        /// </param>
        /// <param name="active">
        /// Initial value of the <see cref="Active"/> property.
        /// </param>
        public Tenant(Guid tenantId, string name, string description, bool active)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "TenentId is required.");
            AssertionConcern.AssertArgumentNotEmpty(name, "The tenant name is required.");
            AssertionConcern.AssertArgumentLength(name, 1, 100, "The name must be 100 characters or less.");
            AssertionConcern.AssertArgumentNotEmpty(description, "The tenant description is required.");
            AssertionConcern.AssertArgumentLength(description, 1, 100, "The name description be 100 characters or less.");

            this.Id = tenantId;
            //this.TenantId = tenantId;
            this.Name = name;
            this.Description = description;
            this.Active = active;

            this.registrationInvitations = new HashSet<RegistrationInvitation>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tenant"/> class for a derived type,
        /// and otherwise blocks new instances from being created with an empty constructor.
        /// </summary>
        protected Tenant()
        {
            //if (this.Id == Guid.Empty) this.Id = Guid.NewGuid();
            //this.TenantId = new TenantId(this.Id.ToString());
        }

        #endregion

        #region [ Public Properties ]

        //[Key]
        //public Guid Id { get; private set; }

        //public string TenantId_Id { get { return TenantId.Id; } private set {} }
        //public TenantId TenantId { get; private set; }

        public string Name { get; private set; }

        public bool Active { get; private set; }

        public string Description { get; private set; }

        #endregion

        #region [ Command Methods which Publish Domain Events ]

        public void Activate()
        {
            if (!this.Active)
            {
                this.Active = true;
                DomainEventPublisher.Instance.Publish(new TenantActivated(this.Id));
            }
        }

        public void Deactivate()
        {
            if (this.Active)
            {
                this.Active = false;

                DomainEventPublisher.Instance.Publish(new TenantDeactivated(this.Id));
            }
        }

        public bool IsRegistrationAvailableThrough(string invitationIdentifier)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            RegistrationInvitation invitation = this.GetInvitation(invitationIdentifier);

            return ((invitation != null) && invitation.IsAvailable());
        }

        public RegistrationInvitation OfferRegistrationInvitation(string description)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");
            AssertionConcern.AssertArgumentFalse(this.IsRegistrationAvailableThrough(description), "Invitation already exists.");

            RegistrationInvitation invitation = new RegistrationInvitation(this.Id, Guid.NewGuid().ToString(), description);

            AssertionConcern.AssertStateTrue(this.registrationInvitations.Add(invitation), "The invitation should have been added.");

            return invitation;
        }

        public Group ProvisionGroup(string name, string description)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            Group group = new Group(this.Id, name, description);

            DomainEventPublisher.Instance.Publish(new GroupProvisioned(this.Id, name));

            return group;
        }

        public Role ProvisionRole(string name, string description, bool supportsNesting = false)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            Role role = new Role(this.Id, name, description, supportsNesting);

            DomainEventPublisher.Instance.Publish(new RoleProvisioned(this.Id, name));

            return role;
        }

        public RegistrationInvitation RedefineRegistrationInvitationAs(string invitationIdentifier)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            RegistrationInvitation invitation = this.GetInvitation(invitationIdentifier);
            if (invitation != null)
            {
                invitation.RedefineAs().OpenEnded();
            }

            return invitation;
        }

        public User RegisterUser(string invitationIdentifier, string username, string password, Enablement enablement, Person person)
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            User user = null;
            if (this.IsRegistrationAvailableThrough(invitationIdentifier))
            {
                // ensure same tenant
                person.TenantId = this.Id;
                user = new User(this.Id, username, password, enablement, person);
            }

            return user;
        }

        public void WithdrawInvitation(string invitationIdentifier)
        {
            RegistrationInvitation invitation = this.GetInvitation(invitationIdentifier);
            if (invitation != null)
            {
                this.registrationInvitations.Remove(invitation);
            }
        }

        #endregion

        #region [ Additional Methods ]

        public ICollection<InvitationDescriptor> AllAvailableRegistrationInvitations()
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            return this.AllRegistrationInvitationsFor(true);
        }

        public ICollection<InvitationDescriptor> AllUnavailableRegistrationInvitations()
        {
            AssertionConcern.AssertStateTrue(this.Active, "Tenant is not active.");

            return this.AllRegistrationInvitationsFor(false);
        }

        /// <summary>
        /// Returns a string that represents the current entity.
        /// </summary>
        /// <returns>
        /// A unique string representation of an instance of this entity.
        /// </returns>
        public override string ToString()
        {
            const string Format = "Tenant [tenantId={0}, name={1}, description={2}, active={3}]";
            return string.Format(Format, this.Id, this.Name, this.Description, this.Active);
        }

        /// <summary>
        /// Gets the values which identify a <see cref="Tenant"/> entity,
        /// which are the <see cref="TenantId"/> and the <see cref="Name"/>.
        /// </summary>
        /// <returns>
        /// A sequence of values which uniquely identifies an instance of this entity.
        /// </returns>
        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return this.Id;
            yield return this.Name;
        }

        private List<InvitationDescriptor> AllRegistrationInvitationsFor(bool isAvailable)
        {
            return this.registrationInvitations
                .Where(x => (x.IsAvailable() == isAvailable))
                .Select(x => x.ToDescriptor())
                .ToList();
        }

        private RegistrationInvitation GetInvitation(string invitationIdentifier)
        {
            return this.registrationInvitations.FirstOrDefault(x => x.IsIdentifiedBy(invitationIdentifier));
        }

        #endregion
    }
}