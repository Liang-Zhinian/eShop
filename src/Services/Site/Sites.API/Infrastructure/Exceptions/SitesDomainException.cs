using System;

namespace Sites.API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class SitesDomainException : Exception
    {
        public SitesDomainException()
        { }

        public SitesDomainException(string message)
            : base(message)
        { }

        public SitesDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
