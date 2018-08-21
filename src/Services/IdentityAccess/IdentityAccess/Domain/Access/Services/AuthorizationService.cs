
namespace SaaSEqt.IdentityAccess.Domain.Access.Services
{
	using System;

	using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Access.Entities;
    using SaaSEqt.IdentityAccess.Domain.Access.Repositories;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
    using SaaSEqt.IdentityAccess.Domain.Identity.Repositories;
    using SaaSEqt.IdentityAccess.Domain.Identity.Services;

	/// <summary>
	/// A domain service providing methods to determine
	/// whether a <see cref="User"/> has a <see cref="Role"/>.
	/// </summary>
	
	public class AuthorizationService
	{
		#region [ ReadOnly Fields and Constructor ]

		private readonly IUserRepository userRepository;
		private readonly IGroupRepository groupRepository;
		private readonly IRoleRepository roleRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationService"/> class.
		/// </summary>
		/// <param name="userRepository">
		/// An instance of <see cref="IUserRepository"/> to use internally.
		/// </param>
		/// <param name="groupRepository">
		/// An instance of <see cref="IGroupRepository"/> to use internally.
		/// </param>
		/// <param name="roleRepository">
		/// An instance of <see cref="IRoleRepository"/> to use internally.
		/// </param>
		public AuthorizationService(
			IUserRepository userRepository,
			IGroupRepository groupRepository,
			IRoleRepository roleRepository)
		{
			this.groupRepository = groupRepository;
			this.roleRepository = roleRepository;
			this.userRepository = userRepository;
		}

		#endregion

		/// <summary>
		/// Determines whether a <see cref="User"/> has a <see cref="Role"/>,
		/// given the names of the user and the role.
		/// </summary>
		/// <param name="tenantId">
		/// A <see cref="TenantId"/> identifying a <see cref="Tenant"/> with
		/// which a <see cref="User"/> and <see cref="Role"/> are associated.
		/// </param>
		/// <param name="username">
		/// The unique username identifying a <see cref="User"/>.
		/// </param>
		/// <param name="roleName">
		/// The unique name identifying a <see cref="Role"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the <see cref="User"/> has the
		/// <see cref="Role"/>; otherwise, <c>false</c>.
		/// </returns>
        public bool IsUserInRole(Guid tenantId, string username, string roleName)
		{
			AssertionConcern.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
			AssertionConcern.AssertArgumentNotEmpty(username, "Username must not be provided.");
			AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

			User user = this.userRepository.UserWithUsername(tenantId, username);
			return ((user != null) && this.IsUserInRole(user, roleName));
		}

		/// <summary>
		/// Determines whether a <see cref="User"/> has a <see cref="Role"/>,
		/// given the user and the name of the role.
		/// </summary>
		/// <param name="user">
		/// A <see cref="User"/> instance.
		/// </param>
		/// <param name="roleName">
		/// The unique name identifying a <see cref="Role"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the <see cref="User"/> has the
		/// <see cref="Role"/>; otherwise, <c>false</c>.
		/// </returns>
		public bool IsUserInRole(User user, string roleName)
		{
			AssertionConcern.AssertArgumentNotNull(user, "User must not be null.");
			AssertionConcern.AssertArgumentNotEmpty(roleName, "Role name must not be null.");

			bool authorized = false;
			if (user.IsEnabled)
			{
				Role role = this.roleRepository.RoleNamed(user.TenantId, roleName);
				if (role != null)
				{
					authorized = role.IsInRole(user, new GroupMemberService(this.userRepository, this.groupRepository));
				}
			}

			return authorized;
		}
	}
}