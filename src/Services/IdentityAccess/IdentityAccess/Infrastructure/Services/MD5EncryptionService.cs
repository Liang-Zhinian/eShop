
namespace SaaSEqt.IdentityAccess.Infrastructure.Services
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

    using SaaSEqt.Common.Domain.Model;
    using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

	/// <summary>
	/// Implementation of <see cref="IEncryptionService"/>
	/// using an <see cref="MD5"/> hasher to create a
	/// one-way hash of a plain text string.
	/// </summary>
	
	public class MD5EncryptionService : IEncryptionService
	{
		/// <summary>
		/// Creates a one-way MD5 has of a given plain text string.
		/// </summary>
		/// <param name="plainTextValue">
		/// A plain text string to be hashed.
		/// </param>
		/// <returns>
		/// The one-way MD5 has of a given <paramref name="plainTextValue"/>.
		/// </returns>
		public string EncryptedValue(string plainTextValue)
		{
			AssertionConcern.AssertArgumentNotEmpty(plainTextValue, "Plain text value to encrypt must be provided.");

			StringBuilder encryptedValue = new StringBuilder();
			MD5 hasher = MD5.Create();
			byte[] data = hasher.ComputeHash(Encoding.Default.GetBytes(plainTextValue));

			foreach (byte t in data)
			{
				// The format string indicates "hexadecimal with a precision of two digits"
				// https://msdn.microsoft.com/en-us/library/dwhawy9k%28v=vs.110%29.aspx
				encryptedValue.Append(t.ToString("x2"));
			}

			return encryptedValue.ToString();
		}
	}
}