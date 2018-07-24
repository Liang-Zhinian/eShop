namespace SaaSEqt.IdentityAccess.Domain.Identity.Entities
{
	using System;

	/// <summary>
	/// Enumeration of the types of items which may
	/// be placed within a <see cref="Group"/>.
	/// </summary>
	
	public enum GroupMemberType : byte
	{
		/// <summary>
		/// Indicates that the group member is a <see cref="Group"/>.
		/// </summary>
		Group = 0,

		/// <summary>
		/// Indicates that the group member is a <see cref="User"/>.
		/// </summary>
		User = 1
	}
}