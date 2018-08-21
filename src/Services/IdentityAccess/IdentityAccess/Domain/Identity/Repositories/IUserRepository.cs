
namespace SaaSEqt.IdentityAccess.Domain.Identity.Repositories
{
    using System;
    using SaaSEqt.IdentityAccess.Domain.Identity.Entities;

	/// <summary>
	/// Contract for a collection-oriented repository of <see cref="User"/> entities.
	/// </summary>
	/// <remarks>
	/// Because this is a collection-oriented repository, the <see cref="Add"/>
	/// method needs to be called no more than once per stored entity.
	/// Subsequent changes to any stored <see cref="User"/> are implicitly
	/// persisted, and adding the same entity a second time will have no effect.
	/// </remarks>
	
	public interface IUserRepository
	{
		/// <summary>
		/// Stores a given <see cref="User"/> in the repository.
		/// </summary>
		/// <param name="user">
		/// The instance of <see cref="User"/> to store.
		/// </param>
		/// <remarks>
		/// Because this is a collection-oriented repository, the <see cref="Add"/>
		/// method needs to be called no more than once per stored entity.
		/// Subsequent changes to any stored <see cref="User"/> are implicitly
		/// persisted, and adding the same entity a second time will have no effect.
		/// </remarks>
		void Add(User user);

		/// <summary>
		/// Retrieves a <see cref="User"/> from the repository
		/// based on a username and password for authentication,
		/// and implicitly persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
		/// The identifier of a <see cref="Tenant"/> to which
		/// a <see cref="User"/> may belong, corresponding
		/// to its <see cref="User.TenantId"/>.
		/// </param>
		/// <param name="username">
		/// The unique name of a <see cref="User"/>, matching
		/// the value of its <see cref="User.Username"/>.
		/// </param>
		/// <param name="encryptedPassword">
		/// A one-way hash of the password paired with
		/// the <paramref name="username"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="User"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
        User UserFromAuthenticCredentials(Guid tenantId, string username, string encryptedPassword);

		/// <summary>
		/// Retrieves a <see cref="User"/> from the repository
		/// based only on a username, when authentication
		/// is not needed or already assumed, and implicitly
		/// persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
		/// The identifier of a <see cref="Tenant"/> to which
		/// a <see cref="User"/> may belong, corresponding
		/// to its <see cref="User.TenantId"/>.
		/// </param>
		/// <param name="username">
		/// The unique name of a <see cref="User"/>, matching
		/// the value of its <see cref="User.Username"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="User"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
        User UserWithUsername(Guid tenantId, string username);
	}
}