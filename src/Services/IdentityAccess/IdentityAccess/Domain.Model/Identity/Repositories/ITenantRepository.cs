
namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Repositories
{
    using System;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

	/// <summary>
	/// Contract for a collection-oriented repository of <see cref="Tenant"/> entities.
	/// </summary>
	/// <remarks>
	/// Because this is a collection-oriented repository, the <see cref="Add"/>
	/// method needs to be called no more than once per stored entity.
	/// Subsequent changes to any stored <see cref="Tenant"/> are implicitly
	/// persisted, and adding the same entity a second time will have no effect.
	/// </remarks>
	
	public interface ITenantRepository
	{
		/// <summary>
		/// Creates an identifier to use as the value of the
		/// <see cref="Tenant.TenantGuidId"/> property for
		/// a new instance of <see cref="Tenant"/>
		/// before the entity is stored in the repository.
		/// </summary>
		/// <returns>
		/// A <see cref="TenantId"/> value to use to identify
		/// a new instance of <see cref="Tenant"/>.
		/// </returns>
        Guid GetNextIdentity();

		/// <summary>
		/// Removes a given <see cref="Tenant"/> from the repository.
		/// </summary>
		/// <param name="tenant">
		/// The instance of <see cref="Tenant"/> to remove.
		/// </param>
		void Remove(Tenant tenant);

		/// <summary>
		/// Stores a given <see cref="Tenant"/> in the repository.
		/// </summary>
		/// <param name="tenant">
		/// The instance of <see cref="Tenant"/> to store.
		/// </param>
		/// <remarks>
		/// Because this is a collection-oriented repository, the <see cref="Add"/>
		/// method needs to be called no more than once per stored entity.
		/// Subsequent changes to any stored <see cref="Tenant"/> are implicitly
		/// persisted, and adding the same entity a second time will have no effect.
		/// </remarks>
		void Add(Tenant tenant);

		/// <summary>
		/// Retrieves a <see cref="Tenant"/> from the repository,
		/// and implicitly persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
		/// The identifier of a <see cref="Tenant"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="Tenant"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
        Tenant Get(Guid tenantId);

		/// <summary>
		/// Retrieves a <see cref="Tenant"/> from the repository
		/// based on its name, and implicitly persists any changes
		/// to the retrieved entity.
		/// </summary>
		/// <param name="name">
		/// The unique name of a <see cref="Tenant"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="Tenant"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
		Tenant GetByName(string name);
	}
}