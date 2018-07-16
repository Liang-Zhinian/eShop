using System;

namespace SaaSEqt.eShop.Services.SchedulableCatalog.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class SchedulableCatalogDomainException : Exception
    {
        public SchedulableCatalogDomainException()
        { }

        public SchedulableCatalogDomainException(string message)
            : base(message)
        { }

        public SchedulableCatalogDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
