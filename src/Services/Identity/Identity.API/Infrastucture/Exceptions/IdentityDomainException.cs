using System;

namespace Identity.API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class IdentityDomainException : Exception
    {
        public IdentityDomainException()
        { }

        public IdentityDomainException(string message)
            : base(message)
        { }

        public IdentityDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
