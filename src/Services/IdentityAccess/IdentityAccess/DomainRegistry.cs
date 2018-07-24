
namespace SaaSEqt.IdentityAccess
{
	using System;

	using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
    using SaaSEqt.IdentityAccess.Domain.Identity.Services;

	/// <summary>
	/// Holds static references to domain services
	/// which would normally be configured by an
	/// Inversion of Control container.
	/// </summary>
	
	public static class DomainRegistry
	{
		/// <summary>
		/// Gets the instance of <see cref="IEncryptionService"/> to use.
		/// </summary>
		public static IEncryptionService EncryptionService
		{
			get
			{
				// this is not a desirable dependency since it
				// references port adapters, but it doesn't
				// require an IoC container
				return new Infrastructure.Services.MD5EncryptionService();
			}
		}

		/// <summary>
		/// Gets an instance of a domain service which generates
		/// passwords and evaluates passwords for strength.
		/// </summary>
		public static PasswordService PasswordService
		{
			get { return new PasswordService(); }
		}
	}
}