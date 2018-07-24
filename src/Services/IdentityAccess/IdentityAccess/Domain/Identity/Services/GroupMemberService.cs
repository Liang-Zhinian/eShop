
namespace SaaSEqt.IdentityAccess.Domain.Identity.Services
{
	using System;

    using System.Linq;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
    using SaaSEqt.IdentityAccess.Domain.Identity.Repositories;
    using SaaSEqt.IdentityAccess.Domain.Repositories;

	/// <summary>
	/// A domain service providing methods to determine
	/// whether a <see cref="User"/> or <see cref="Group"/>
	/// is a member of a group or of a nested group.
	/// </summary>
	
	public class GroupMemberService
	{
		#region [ ReadOnly Fields and Constructor ]

		// The maximum value of a signed byte should be 127.
		private const int MaxGroupNestingRecursion = sbyte.MaxValue;

		private readonly IGroupRepository groupRepository;
		private readonly IUserRepository userRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="GroupMemberService"/> class.
		/// </summary>
		/// <param name="userRepository">
		/// An instance of <see cref="IUserRepository"/> to use internally.
		/// </param>
		/// <param name="groupRepository">
		/// An instance of <see cref="IGroupRepository"/> to use internally.
		/// </param>
		public GroupMemberService(
			IUserRepository userRepository,
			IGroupRepository groupRepository)
		{
			this.groupRepository = groupRepository;
			this.userRepository = userRepository;
		}

		#endregion

		#region [ Public Methods ]

		/// <summary>
		/// Determines whether a <see cref="User"/>'s declared
		/// membership in a <see cref="Group"/> is valid.
		/// </summary>
		/// <param name="group">
		/// An instance of <see cref="Group"/> which may have
		/// the <paramref name="user"/> as a member.
		/// </param>
		/// <param name="user">
		/// An instance of <see cref="User"/> which may be
		/// a member of the <paramref name="group"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the <paramref name="user"/>'s
		/// <see cref="User.TenantId"/> matches that of
		/// the <paramref name="group"/> and the user's
		/// <see cref="User.IsEnabled"/> property is true;
		/// otherwise, <c>false</c>.
		/// </returns>
		public bool ConfirmUser(Group group, User user)
		{
			User confirmedUser = this.userRepository.UserWithUsername(group.TenantId, user.Username);

			return ((confirmedUser == null) || (!confirmedUser.IsEnabled));
		}

		/// <summary>
		/// Recursive function which determines whether
		/// a <see cref="Group"/> is a member of a group
		/// or of a descendant group.
		/// </summary>
		/// <param name="group">
		/// An instance of <see cref="Group"/> to check for
		/// the presence of <paramref name="memberGroup"/>
		/// among its members or descendants.
		/// </param>
		/// <param name="memberGroup">
		/// Another group which may potentially be added to the
		/// members of <paramref name="group"/> if it's allowed.
		/// </param>
		/// <returns>
		/// <c>true</c> if the given <paramref name="memberGroup"/>
		/// is a member of the given <paramref name="group"/> or of
		/// a descendant group; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMemberGroup(Group group, GroupMember memberGroup)
		{
			return this.IsMemberGroup(group, memberGroup, 0);
		}

		/// <summary>
		/// Determines whether a <see cref="User"/> is a member
		/// of the <see cref="Group"/> members of a group.
		/// </summary>
		/// <param name="group">
		/// An instance of <see cref="Group"/> having
		/// members which are groups.
		/// </param>
		/// <param name="user">
		/// An instance of <see cref="User"/> which may be a member
		/// of group members of the given <paramref name="group"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if the given <paramref name="user"/>
		/// is a member of the groups nested within the given
		/// <paramref name="group"/>; otherwise, <c>false</c>.
		/// </returns>
		public bool IsUserInNestedGroup(Group group, User user)
		{
			foreach (GroupMember member in group.GroupMembers.Where(x => x.IsGroup))
			{
				Group nestedGroup = this.groupRepository.GroupNamed(member.TenantId, member.Name);
				if (nestedGroup != null)
				{
					bool isInNestedGroup = nestedGroup.IsMember(user, this);
					if (isInNestedGroup)
					{
						return true;
					}
				}
			}

			return false;
		}

		#endregion

		#region [ Private Recursive Method with Overflow Catch ]

		private bool IsMemberGroup(Group group, GroupMember memberGroup, int recursionCount)
		{
			if (recursionCount > MaxGroupNestingRecursion)
			{
				throw new InvalidOperationException("The maximum depth of group nesting has been exceeded, stopping recursive function.");
			}

			bool isMember = false;
			foreach (GroupMember member in group.GroupMembers.Where(x => x.IsGroup))
			{
				if (memberGroup.Equals(member))
				{
					isMember = true;
				}
				else
				{
					Group nestedGroup = this.groupRepository.GroupNamed(member.TenantId, member.Name);
					if (nestedGroup != null)
					{
						int nextRecursionCount = (recursionCount + 1);

						isMember = this.IsMemberGroup(nestedGroup, memberGroup, nextRecursionCount);
					}
				}

				if (isMember)
				{
					break;
				}
			}

			return isMember;
		}

		#endregion
	}
}