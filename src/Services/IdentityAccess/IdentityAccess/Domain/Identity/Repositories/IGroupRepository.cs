
namespace SaaSEqt.IdentityAccess.Domain.Identity.Repositories
{
    using System;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

	/// <summary>
	/// Contract for a collection-oriented repository of <see cref="Group"/> entities.
	/// </summary>
	/// <remarks>
	/// Because this is a collection-oriented repository, the <see cref="Add"/>
	/// method needs to be called no more than once per stored entity.
	/// Subsequent changes to any stored <see cref="Group"/> are implicitly
	/// persisted, and adding the same entity a second time will have no effect.
	/// </remarks>
	
	public interface IGroupRepository
	{
		/// <summary>
		/// Stores a given <see cref="Group"/> in the repository.
		/// </summary>
		/// <param name="group">
		/// The instance of <see cref="Group"/> to store.
		/// </param>
		/// <remarks>
		/// Because this is a collection-oriented repository, the <see cref="Add"/>
		/// method needs to be called no more than once per stored entity.
		/// Subsequent changes to any stored <see cref="Group"/> are implicitly
		/// persisted, and adding the same entity a second time will have no effect.
		/// </remarks>
		void Add(Group group);

		/// <summary>
		/// Retrieves a <see cref="Group"/> from the repository,
		/// and implicitly persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
		/// The identifier of a <see cref="Tenant"/> to which
		/// a <see cref="Group"/> may belong, corresponding
		/// to its <see cref="Group.TenantId"/>.
		/// </param>
		/// <param name="groupName">
		/// The name of a <see cref="Group"/>, matching
		/// the value of its <see cref="Group.Name"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="Group"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
        Group GroupNamed(Guid tenantId, string groupName);
	}
}