namespace SaaSEqt.IdentityAccess.Application
{
	using System;

	using SaaSEqt.IdentityAccess.Application.Commands;
    using SaaSEqt.IdentityAccess.Domain.Access.Entities;
    using SaaSEqt.IdentityAccess.Domain.Access.Services;
    using SaaSEqt.IdentityAccess.Domain.Access.Repositories;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
    using SaaSEqt.IdentityAccess.Domain.Identity.Services;
    using SaaSEqt.IdentityAccess.Domain.Identity.Repositories;
    using SaaSEqt.Common.Domain.Model;

    public sealed class AccessApplicationService
	{
        private readonly IUnitOfWork unitOfWork;
		private readonly IGroupRepository groupRepository;
		private readonly IRoleRepository roleRepository;
		private readonly ITenantRepository tenantRepository;
		private readonly IUserRepository userRepository;

		public AccessApplicationService(
            IUnitOfWork unitOfWork,
			IGroupRepository groupRepository,
			IRoleRepository roleRepository,
			ITenantRepository tenantRepository,
			IUserRepository userRepository)
		{
            this.unitOfWork = unitOfWork;
			this.groupRepository = groupRepository;
			this.roleRepository = roleRepository;
			this.tenantRepository = tenantRepository;
			this.userRepository = userRepository;
		}

		public void AssignUserToRole(AssignUserToRoleCommand command)
		{
			var tenantId = new TenantId(command.TenantId);
			var user = this.userRepository.UserWithUsername(tenantId, command.Username);
			if (user != null)
			{
				var role = this.roleRepository.RoleNamed(tenantId, command.RoleName);
				if (role != null)
				{
					role.AssignUser(user);
				}
            }
            this.unitOfWork.Commit();
		}

		public bool IsUserInRole(string tenantId, string userName, string roleName)
		{
			return UserInRole(tenantId, userName, roleName) != null;
		}

		public User UserInRole(string tenantId, string userName, string roleName)
		{
			var id = new TenantId(tenantId);
			var user = this.userRepository.UserWithUsername(id, userName);
			if (user != null)
			{
				var role = this.roleRepository.RoleNamed(id, roleName);
				if (role != null)
				{
					if (role.IsInRole(user, new GroupMemberService(this.userRepository, this.groupRepository)))
					{
						return user;
					}
				}
			}

			return null;
		}

		public void ProvisionRole(ProvisionRoleCommand command)
		{
			var tenantId = new TenantId(command.TenantId);
			var tenant = this.tenantRepository.Get(tenantId);
			var role = tenant.ProvisionRole(command.RoleName, command.Description, command.SupportsNesting);
            this.roleRepository.Add(role);
            this.unitOfWork.Commit();
		}
	}
}