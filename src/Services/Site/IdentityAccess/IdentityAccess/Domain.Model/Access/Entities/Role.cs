﻿
namespace SaaSEqt.IdentityAccess.Domain.Model.Access.Entities
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Model.Access.Events;
    using SaaSEqt.IdentityAccess.Domain.Model.Access.Services;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Services;

    /// <summary>
    /// An entity representing an authentication role for a
    /// particular <see cref="Tenant"/>, which determines
    /// the types of access granted or denied to a
    /// <see cref="User"/> or <see cref="Group"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class borrows functionality from an internal
    /// instance of <see cref="Group"/> to be able to
    /// assign users and groups to this role.
    /// </para>
    /// <para>
    /// A role might also be called a "claim" in some
    /// authentication approaches.
    /// </para>
    /// </remarks>

    public class Role : EntityWithCompositeId
    {
        #region [ Fields and Constructor Overloads ]

        /// <summary>
        /// An internal instance of <see cref="Group"/>
        /// which provides functionality for assigning
        /// users and groups to this role.
        /// </summary>
        private readonly Group internalGroup;

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
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
        /// <param name="supportsNesting">
        /// Initial value of the <see cref="SupportsNesting"/> property.
        /// </param>
        public Role(Guid tenantId, string name, string description, bool supportsNesting)
        {
            // Defer validation to the property setters.
            this.Description = description;
            this.Name = name;
            this.SupportsNesting = supportsNesting;
            this.TenantId = tenantId;

            this.internalGroup = this.CreateInternalGroup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class for a derived type,
        /// and otherwise blocks new instances from being created with an empty constructor.
        /// </summary>
        protected Role()
        {
            //this.Id = Guid.NewGuid();
        }

        #endregion

        #region [ Public Properties ]

        //[Key]
        //public Guid Id { get; private set; }

        public string Description { get; private set; }

        public string Name { get; private set; }

        public bool SupportsNesting { get; private set; }

        public Guid TenantId { get; private set; }
        //[NotMapped]
        public virtual Tenant Tenant { get; set; }

        public Guid GroupId { get { return this.internalGroup.Id; } private set {} }
        [NotMapped]
        public virtual Group Group
        {
            get
            {
                return this.internalGroup;
            }
            private set { }
        }

		#endregion

		#region [ Command Methods which Publish Domain Events ]

		public void AssignGroup(Group group, GroupMemberService groupMemberService)
		{
			AssertionConcern.AssertStateTrue(this.SupportsNesting, "This role does not support group nesting.");
			AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
			AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");

			this.internalGroup.AddGroup(group, groupMemberService);

			DomainEventPublisher
				.Instance
				.Publish(new GroupAssignedToRole(
						this.TenantId,
						this.Name,
						group.Name));
		}

		public void AssignUser(User user)
		{
			AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
			AssertionConcern.AssertArgumentEquals(this.TenantId, user.TenantId, "Wrong tenant for this user.");

			this.internalGroup.AddUser(user);

			DomainEventPublisher
				.Instance
				.Publish(new UserAssignedToRole(
						this.TenantId,
						this.Name,
						user.Username,
						user.Person.Name.FirstName,
						user.Person.Name.LastName,
						user.Person.EmailAddress.Address));
		}

		public void UnassignGroup(Group group)
		{
			AssertionConcern.AssertStateTrue(this.SupportsNesting, "This role does not support group nesting.");
			AssertionConcern.AssertArgumentNotNull(group, "Group must not be null.");
			AssertionConcern.AssertArgumentEquals(this.TenantId, group.TenantId, "Wrong tenant for this group.");

			this.internalGroup.RemoveGroup(group);

			DomainEventPublisher
				.Instance
				.Publish(new GroupUnassignedFromRole(
						this.TenantId,
						this.Name,
						group.Name));
		}

		public void UnassignUser(User user)
		{
			AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
			AssertionConcern.AssertArgumentEquals(this.TenantId, user.TenantId, "Wrong tenant for this user.");

			this.internalGroup.RemoveUser(user);

			DomainEventPublisher
				.Instance
				.Publish(new UserUnassignedFromRole(
						this.TenantId,
						this.Name,
						user.Username));
		}

		#endregion

		#region [ Additional Methods ]

		/// <summary>
		/// Uses a <see cref="GroupMemberService"/> to determine
		/// whether a given <see cref="User"/> has this role, including
		/// by way of nested <see cref="Group"/> membership.
		/// </summary>
		/// <param name="user">
		/// A <see cref="User"/> entity to check.
		/// </param>
		/// <param name="groupMemberService">
		/// The instance of <see cref="GroupMemberService"/>
		/// relayed to the <see cref="Group.IsMember"/> method.
		/// </param>
		/// <returns>
		/// <c>true</c> if the given <paramref name="user"/>
		/// has this role; otherwise, <c>false</c>.
		/// </returns>
		public bool IsInRole(User user, GroupMemberService groupMemberService)
		{
			return this.internalGroup.IsMember(user, groupMemberService);
		}

		/// <summary>
		/// Returns a string that represents the current entity.
		/// </summary>
		/// <returns>
		/// A unique string representation of an instance of this entity.
		/// </returns>
		public override string ToString()
		{
			const string Format = "Role [tenantId={0}, name={1}, description={2}, supportsNesting={3}, group={4}]";
			return string.Format(Format, this.TenantId, this.Name, this.Description, this.SupportsNesting, this.internalGroup);
		}

		/// <summary>
		/// Gets the values which identify a <see cref="Role"/> entity,
		/// which are the <see cref="TenantId"/> and the <see cref="Name"/>.
		/// </summary>
		/// <returns>
		/// A sequence of values which uniquely identifies an instance of this entity.
		/// </returns>
		protected override IEnumerable<object> GetIdentityComponents()
		{
			yield return this.TenantId;
			yield return this.Name;
		}

		private Group CreateInternalGroup()
		{
			string groupName = string.Concat(Group.RoleGroupPrefix, Guid.NewGuid());
			return new Group(this.TenantId, groupName, string.Concat("Role backing group for: ", this.Name));
		}

		#endregion
	}
}