namespace SaaSEqt.eShop.Services.Marketing.API.Infrastructure.Exceptions
{
    using System;

    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class MarketingDomainException : Exception
    {
        public MarketingDomainException()
        { }

        public MarketingDomainException(string message)
            : base(message)
        { }

        public MarketingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}