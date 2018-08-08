using System;

namespace Branding.API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class BrandingDomainException : Exception
    {
        public BrandingDomainException()
        { }

        public BrandingDomainException(string message)
            : base(message)
        { }

        public BrandingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
