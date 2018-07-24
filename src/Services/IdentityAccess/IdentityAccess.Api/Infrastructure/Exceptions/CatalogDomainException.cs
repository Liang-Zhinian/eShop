using System;

namespace SaaSEqt.IdentityAccess.API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class IdentityAccessDomainException : Exception
    {
        public IdentityAccessDomainException()
        { }

        public IdentityAccessDomainException(string message)
            : base(message)
        { }

        public IdentityAccessDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
