
namespace SaaSEqt.IdentityAccess
{
	using System;

	/// <summary>
	/// Contract for a domain service which encrypts
	/// a plain text string to use as a password.
	/// </summary>
	
	public interface IEncryptionService
	{
		/// <summary>
		/// Encrypts a given plain text string and returns the cipher text.
		/// Typically, the returned value should be a one-way hash of the
		/// given <paramref name="plainTextValue"/>, and not cipher text
		/// which could be decrypted with a key.
		/// </summary>
		/// <param name="plainTextValue">
		/// A plain text string representing a password,
		/// to be hashed before it is stored.
		/// </param>
		/// <returns>
		/// A string which is one-way cryptographic hash
		/// of the given <paramref name="plainTextValue"/>.
		/// </returns>
		string EncryptedValue(string plainTextValue);
	}
}