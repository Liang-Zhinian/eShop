namespace SaaSEqt.IdentityAccess.Application
{
	using System;

    using SaaSEqt.IdentityAccess.Application.Commands;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
    using SaaSEqt.IdentityAccess.Domain.Identity.Services;
    using SaaSEqt.IdentityAccess.Domain.Identity.Repositories;
    using SaaSEqt.Common.Domain.Model;
    using System.Threading.Tasks;

    /// <summary>
    /// Coordinates interactions among entities in the "Domain.Model.Identity" namespace.
    /// </summary>

    public class IdentityApplicationService
    {
        readonly IUnitOfWork unitOfWork;
        readonly AuthenticationService authenticationService;
        readonly GroupMemberService groupMemberService;
        readonly IGroupRepository groupRepository;
        readonly TenantProvisioningService tenantProvisioningService;
        readonly ITenantRepository tenantRepository;
        readonly IUserRepository userRepository;

		public IdentityApplicationService (
            IUnitOfWork unitOfWork,
			AuthenticationService authenticationService,
			GroupMemberService groupMemberService,
			IGroupRepository groupRepository,
			TenantProvisioningService tenantProvisioningService,
			ITenantRepository tenantRepository,
			IUserRepository userRepository
		){
            this.unitOfWork = unitOfWork;
			this.authenticationService = authenticationService;
			this.groupMemberService= groupMemberService;
			this.groupRepository =groupRepository;
			this.tenantProvisioningService= tenantProvisioningService;
			this.tenantRepository =tenantRepository;
			this.userRepository= userRepository;
		}

        public void ActivateTenant(ActivateTenantCommand command)
        {
            var tenant = GetExistingTenant(command.TenantId);
            tenant.Activate();
            this.unitOfWork.Commit();
        }

        public void AddGroupToGroup(AddGroupToGroupCommand command)
        {
            var parentGroup = GetExistingGroup(command.TenantId, command.ParentGroupName);
            var childGroup = GetExistingGroup(command.TenantId, command.ChildGroupName);
            parentGroup.AddGroup(childGroup, this.groupMemberService);
            this.unitOfWork.Commit();
        }

        public void AddUserToGroup(AddUserToGroupCommand command)
        {
            var group = GetExistingGroup(command.TenantId, command.GroupName);
            var user = GetExistingUser(command.TenantId, command.Username);
            group.AddUser(user);
            this.unitOfWork.Commit();
        }

        public UserDescriptor AuthenticateUser(AuthenticateUserCommand command)
        {
            return this.authenticationService.Authenticate(Guid.Parse(command.TenantId), command.Username, command.Password);
        }

        public void DeactivateTenant(DeactivateTenantCommand command)
        {
            var tenant = GetExistingTenant(command.TenantId);
            tenant.Deactivate();
            this.unitOfWork.Commit();
        }

        public void ChangeUserContactInformation(ChangeContactInfoCommand command)
        {
            var user = GetExistingUser(command.TenantId, command.Username);
            user.Person.ChangeContactInformation(
                new ContactInformation(
                    new EmailAddress(command.EmailAddress),
                    new PostalAddress(
                        command.AddressStreetAddress,
                        command.AddressCity,
                        command.AddressStateProvince,
                        command.AddressPostalCode,
                        command.AddressCountryCode),
                    new Telephone(command.PrimaryTelephone),
                    new Telephone(command.SecondaryTelephone)));
            this.unitOfWork.Commit();
        }

        public void ChangeUserEmailAddress(ChangeEmailAddressCommand command)
        {
            var user = GetExistingUser(command.TenantId, command.Username);
            user.Person.ContactInformation.ChangeEmailAddress(new EmailAddress(command.EmailAddress));
            this.unitOfWork.Commit();
        }

        public void ChangeUserPostalAddress(ChangePostalAddressCommand command)
        {
            var user = GetExistingUser(command.TenantId, command.Username);
            user.Person.ContactInformation.ChangePostalAddress(
                new PostalAddress(
                    command.AddressStreetAddress,
                    command.AddressCity,
                    command.AddressStateProvince,
                    command.AddressPostalCode,
                    command.AddressCountryCode));
            this.unitOfWork.Commit();
        }

        public void ChangeUserPrimaryTelephone(ChangePrimaryTelephoneCommand command)
        {
            var user = GetExistingUser(command.Telephone, command.Username);
            user.Person.ContactInformation.ChangePrimaryTelephone(
                new Telephone(command.Telephone)); 
            this.unitOfWork.Commit();
        }

        public void ChangeUserSecondaryTelephone(ChangeSecondaryTelephoneCommand command)
        {
            var user = GetExistingUser(command.Telephone, command.Username);
            user.Person.ContactInformation.ChangeSecondaryTelephone(
                new Telephone(command.Telephone));
            this.unitOfWork.Commit();
        }

        public void ChangeUserPassword(ChangeUserPasswordCommand command)
        {
            var user = GetExistingUser(command.TenantId, command.Username);
            user.ChangePassword(command.CurrentPassword, command.ChangedPassword);
            this.unitOfWork.Commit();
        }

        public void ChangeUserPersonalName(ChangeUserPersonalNameCommand command)
        {
            var user = GetExistingUser(command.TenantId, command.Username);
            user.ChangePersonalName(new FullName(command.FirstName, command.LastName));
            this.unitOfWork.Commit();
        }

        public void DefineUserEnablement(DefineUserEnablementCommand command)
        {
            var user = GetExistingUser(command.TenantId, command.Username);
            user.DefineEnablement(new Enablement(command.Enabled, command.StartDate, command.EndDate));
            this.unitOfWork.Commit();
        }

        public Group GetGroup(string tenantId, string groupName)
        {
            return this.groupRepository.GroupNamed(Guid.Parse(tenantId), groupName);
        }

        public bool IsGroupMember(string tenantId, string groupName, string userName)
        {
            var group = GetExistingGroup(tenantId, groupName);
            var user = GetExistingUser(tenantId, userName);
            return group.IsMember(user, this.groupMemberService);
        }

        public Group ProvisionGroup(ProvisionGroupCommand command)
        {
            var tenant = GetExistingTenant(command.TenantId);
            var group = tenant.ProvisionGroup(command.GroupName, command.Description);
            this.groupRepository.Add(group);
            this.unitOfWork.Commit();
            return group;
        }

        public Task<Tenant> ProvisionTenant(ProvisionTenantCommand command)
        {
            Tenant tenant = this.tenantProvisioningService.ProvisionTenant(
                command.TenantName,
                command.TenantDescription,
                new FullName(command.AdministorFirstName, command.AdministorLastName),
                new EmailAddress(command.EmailAddress),
                new PostalAddress(
                    command.AddressStreetAddress,
                    command.AddressCity,
                    command.AddressStateProvince,
                    command.AddressPostalCode,
                    command.AddressCountryCode),
                new Telephone(command.PrimaryTelephone),
                new Telephone(command.SecondaryTelephone));

            this.unitOfWork.Commit();
            return Task.FromResult(tenant);
        }

        public User RegisterUser(RegisterUserCommand command)
        {
            var tenant = GetExistingTenant(command.TenantId);
            var user = tenant.RegisterUser(
                command.InvitationIdentifier,
                command.Username,
                command.Password,
                new Enablement(command.Enabled, command.StartDate, command.EndDate),
                new Person(
                    Guid.Parse(command.TenantId),
                    new FullName(command.FirstName, command.LastName),
                    new ContactInformation(
                        new EmailAddress(command.EmailAddress),
                        new PostalAddress(
                            command.AddressStreetAddress,
                            command.AddressCity,
                            command.AddressStateProvince,
                            command.AddressPostalCode,
                            command.AddressCountryCode),
                        new Telephone(command.PrimaryTelephone),
                        new Telephone(command.SecondaryTelephone))));

            if (user == null)
                throw new InvalidOperationException("User not registered.");

            this.userRepository.Add(user);
            this.unitOfWork.Commit();

            return user;
        }

        public void RemoveGroupFromGroup(RemoveGroupFromGroupCommand command)
        {
            var parentGroup = GetExistingGroup(command.TenantId, command.ParentGroupName);
            var childGroup = GetExistingGroup(command.TenantId, command.ChildGroupName);
            parentGroup.RemoveGroup(childGroup);
            this.unitOfWork.Commit();
        }

        public void RemoveUserFromGroup(RemoveUserFromGroupCommand command)
        {
            var group = GetExistingGroup(command.TenantId, command.GroupName);
            var user = GetExistingUser(command.TenantId, command.Username);
            group.RemoveUser(user);
            this.unitOfWork.Commit();
        }

        public User GetUser(string tenantId, string userName)
        {
            return this.userRepository.UserWithUsername(Guid.Parse(tenantId), userName);
        }

        User GetExistingUser(string tenantId, string userName)
        {
            var user = GetUser(tenantId, userName);
            if (user == null)
                throw new ArgumentException(
                    string.Format("User does not exist for {0} and {1}.", tenantId, userName));
            return user;
        }

        Group GetExistingGroup(string tenantId, string groupName)
        {
            var group = GetGroup(tenantId, groupName);
            if (group == null)
                throw new ArgumentException(
                    string.Format("Group does not exist for {0} and {1}.", tenantId, groupName));
            return group;
        }

        public Tenant GetTenant(string tenantId)
        {
            return this.tenantRepository.Get(Guid.Parse(tenantId));
        }

        Tenant GetExistingTenant(string tenantId)
        {
            var tenant = GetTenant(tenantId);
            if (tenant == null)
                throw new ArgumentException(
                    string.Format("Tenant does not exist for: {0}", tenantId));
            return tenant;
        }

        public UserDescriptor GetUserDescriptor(string tenantId, string userName)
        {
            var user = GetUser(tenantId, userName);
            if (user != null)
            {
                return user.UserDescriptor;
            }
            else
            {
                return null;
            }
        }
    }
}