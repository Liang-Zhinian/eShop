
namespace SaaSEqt.IdentityAccess.Domain.Model.Identity.Repositories
{
    using System;
    using System.Linq;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

	/// <summary>
    /// Contract for a collection-oriented repository of <see cref="RegistrationInvitation"/> entities.
	/// </summary>
	/// <remarks>
	/// Because this is a collection-oriented repository, the <see cref="Add"/>
	/// method needs to be called no more than once per stored entity.
    /// Subsequent changes to any stored <see cref="RegistrationInvitation"/> are implicitly
	/// persisted, and adding the same entity a second time will have no effect.
	/// </remarks>
	
    public interface IRegistrationInvitationRepository
	{
		/// <summary>
		/// Creates an identifier to use as the value of the
        /// <see cref="RegistrationInvitation.RegistrationInvitationGuidId"/> property for
        /// a new instance of <see cref="RegistrationInvitation"/>
		/// before the entity is stored in the repository.
		/// </summary>
		/// <returns>
        /// A <see cref="RegistrationInvitationId"/> value to use to identify
        /// a new instance of <see cref="RegistrationInvitation"/>.
		/// </returns>
        Guid GetNextIdentity();

		/// <summary>
        /// Removes a given <see cref="RegistrationInvitation"/> from the repository.
		/// </summary>
        /// <param name="registrationInvitation">
        /// The instance of <see cref="RegistrationInvitation"/> to remove.
		/// </param>
        void Remove(RegistrationInvitation registrationInvitation);

		/// <summary>
        /// Stores a given <see cref="RegistrationInvitation"/> in the repository.
		/// </summary>
        /// <param name="registrationInvitation">
        /// The instance of <see cref="RegistrationInvitation"/> to store.
		/// </param>
		/// <remarks>
		/// Because this is a collection-oriented repository, the <see cref="Add"/>
		/// method needs to be called no more than once per stored entity.
        /// Subsequent changes to any stored <see cref="RegistrationInvitation"/> are implicitly
		/// persisted, and adding the same entity a second time will have no effect.
		/// </remarks>
        void Add(RegistrationInvitation registrationInvitation);

		/// <summary>
        /// Retrieves a <see cref="RegistrationInvitation"/> from the repository,
		/// and implicitly persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
        /// The identifier of a <see cref="RegistrationInvitation"/>.
		/// </param>
		/// <returns>
        /// The instance of <see cref="RegistrationInvitation"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
		IQueryable<RegistrationInvitation> GetByTenantId(Guid tenantId);

		/// <summary>
        /// Retrieves a <see cref="RegistrationInvitation"/> from the repository
		/// based on its name, and implicitly persists any changes
		/// to the retrieved entity.
		/// </summary>
		/// <param name="name">
        /// The unique name of a <see cref="RegistrationInvitation"/>.
		/// </param>
		/// <returns>
        /// The instance of <see cref="RegistrationInvitation"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
        RegistrationInvitation GetByDescription(string description);
	}
}