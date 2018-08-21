
namespace SaaSEqt.IdentityAccess.Domain.Access.Repositories
{
	using System;

    using SaaSEqt.IdentityAccess.Domain.Access.Entities;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

    /// <summary>
    /// Contract for a collection-oriented repository of <see cref="Role"/> entities.
    /// </summary>
    /// <remarks>
    /// Because this is a collection-oriented repository, the <see cref="Add"/>
    /// method needs to be called no more than once per stored entity.
    /// Subsequent changes to any stored <see cref="Role"/> are implicitly
    /// persisted, and adding the same entity a second time will have no effect.
    /// </remarks>

    public interface IRoleRepository
	{
		/// <summary>
		/// Stores a given <see cref="Role"/> in the repository.
		/// </summary>
		/// <param name="role">
		/// The instance of <see cref="Role"/> to store.
		/// </param>
		/// <remarks>
		/// Because this is a collection-oriented repository, the <see cref="Add"/>
		/// method needs to be called no more than once per stored entity.
		/// Subsequent changes to any stored <see cref="Role"/> are implicitly
		/// persisted, and adding the same entity a second time will have no effect.
		/// </remarks>
		void Add(Role role);

		/// <summary>
		/// Retrieves a <see cref="Role"/> from the repository,
		/// and implicitly persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
		/// The identifier of a <see cref="Tenant"/> to which
		/// a <see cref="Role"/> may belong, corresponding
		/// to its <see cref="Role.TenantId"/>.
		/// </param>
		/// <param name="roleName">
		/// The name of a <see cref="Role"/>, matching
		/// the value of its <see cref="Role.Name"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="Role"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
        Role RoleNamed(Guid tenantId, string roleName);
	}
}