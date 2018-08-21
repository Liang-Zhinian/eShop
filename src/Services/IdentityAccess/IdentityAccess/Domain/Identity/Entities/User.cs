
namespace SaaSEqt.IdentityAccess.Domain.Identity.Entities
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Identity.Events.User;

	
	public class User : EntityWithCompositeId
	{
		#region [ Fields and Constructor Overloads ]

		private Enablement userEnablement;

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class
		/// and publishes a <see cref="UserRegistered"/> event.
		/// </summary>
		/// <param name="tenantId">
		/// Initial value of the <see cref="TenantId"/> property.
		/// </param>
		/// <param name="username">
		/// Initial value of the <see cref="Username"/> property.
		/// </param>
		/// <param name="password">
		/// Initial value of the <see cref="Password"/> property.
		/// </param>
		/// <param name="enablement">
		/// Initial value of the <see cref="Enablement"/> property.
		/// </param>
		/// <param name="person">
		/// Initial value of the <see cref="Person"/> property.
		/// </param>
		public User(
            Guid tenantId,
			string username,
			string password,
			Enablement enablement,
            Person person)
		{
			AssertionConcern.AssertArgumentNotNull(tenantId, "The tenantId is required.");
			AssertionConcern.AssertArgumentNotNull(person, "The person is required.");
			AssertionConcern.AssertArgumentNotEmpty(username, "The username is required.");
			AssertionConcern.AssertArgumentLength(username, 3, 250, "The username must be 3 to 250 characters.");

            // Defer validation to the property setters.
            this.Id = Guid.NewGuid();
			this.Enablement = enablement;
			this.Person = person;
			this.TenantId = tenantId;
			this.Username = username;

			this.ProtectPassword(string.Empty, password);

			person.User = this;

			DomainEventPublisher
				.Instance
				.Publish(new UserRegistered(
						tenantId,
                    this.Id,
						username,
						person.Name,
						person.ContactInformation.EmailAddress));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class for a derived type,
		/// and otherwise blocks new instances from being created with an empty constructor.
		/// </summary>
		protected User()
        {
            
		}

		#endregion

        #region [ Public Properties ]

        //[Key]
        //public Guid Id { get; private set; }

        //public string TenantId_Id { get { return TenantId.Id; } private set { } }
        public Guid TenantId { get; private set; }

		public bool IsEnabled
		{
			get { return this.Enablement.IsEnablementEnabled(); }
		}

		public Enablement Enablement
		{
			get
			{
				return this.userEnablement;
			}

			private set
			{
				AssertionConcern.AssertArgumentNotNull(value, "The enablement is required.");

				this.userEnablement = value;
			}
		}

		public string Password { get; private set; }

		public Person Person { get; private set; }

		public UserDescriptor UserDescriptor
		{
			get
			{
				return new UserDescriptor(
					this.TenantId,
					this.Username,
					this.Person.EmailAddress.Address);
			}
            private set { }
		}

		public string Username { get; private set; }

        //[NotMapped]
        public virtual Tenant Tenant { get; set; }

		#endregion

		#region [ Command Methods which Publish Domain Events ]

		public void ChangePassword(string currentPassword, string changedPassword)
		{
			AssertionConcern.AssertArgumentNotEmpty(
				currentPassword, "Current and new password must be provided.");

			AssertionConcern.AssertArgumentEquals(
				this.Password, AsEncryptedValue(currentPassword), "Current password not confirmed.");

			this.ProtectPassword(currentPassword, changedPassword);

			DomainEventPublisher
				.Instance
				.Publish(new UserPasswordChanged(
                    this.TenantId,
                    this.Id,
						this.Username));
		}

		public void ChangePersonalContactInformation(ContactInformation contactInformation)
		{
			this.Person.ChangeContactInformation(contactInformation);
		}

		public void ChangePersonalName(FullName personalName)
		{
			this.Person.ChangeName(personalName);
		}

		public void DefineEnablement(Enablement enablement)
		{
			this.Enablement = enablement;

			DomainEventPublisher
				.Instance
				.Publish(new UserEnablementChanged(
                    this.TenantId,
                    this.Id,
						this.Username,
						this.Enablement));
		}

		#endregion

		#region [ Additional Methods ]

		/// <summary>
		/// Returns a string that represents the current entity.
		/// </summary>
		/// <returns>
		/// A unique string representation of an instance of this entity.
		/// </returns>
		public override string ToString()
		{
			const string Format = "User [tenantId={0}, username={1}, person={2}, enablement={3}]";
			return string.Format(Format, this.TenantId, this.Username, this.Person, this.Enablement);
		}

		/// <summary>
		/// Creates a <see cref="GroupMember"/> value of
		/// type <see cref="GroupMemberType.User"/>
		/// based on this <see cref="User"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="GroupMember"/> value of type
		/// <see cref="GroupMemberType.User"/>
		/// based on this <see cref="User"/>.
		/// </returns>
		internal GroupMember ToGroupMember()
		{
			return new GroupMember(this.TenantId, this.Username, GroupMemberType.User);
		}

		/// <summary>
		/// Gets the values which identify a <see cref="User"/> entity,
		/// which are the <see cref="TenantId"/> and the unique <see cref="Username"/>.
		/// </summary>
		/// <returns>
		/// A sequence of values which uniquely identifies an instance of this entity.
		/// </returns>
		protected override IEnumerable<object> GetIdentityComponents()
		{
			yield return this.TenantId;
			yield return this.Username;
		}

		private static string AsEncryptedValue(string plainTextPassword)
		{
			return DomainRegistry.EncryptionService.EncryptedValue(plainTextPassword);
		}

		private void ProtectPassword(string currentPassword, string changedPassword)
		{
			AssertionConcern.AssertArgumentNotEquals(currentPassword, changedPassword, "The password is unchanged.");
			AssertionConcern.AssertArgumentFalse(DomainRegistry.PasswordService.IsWeak(changedPassword), "The password must be stronger.");
			AssertionConcern.AssertArgumentNotEquals(this.Username, changedPassword, "The username and password must not be the same.");

			this.Password = AsEncryptedValue(changedPassword);
		}

		#endregion
	}
}