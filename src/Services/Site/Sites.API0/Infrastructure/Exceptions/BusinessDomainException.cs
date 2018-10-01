using System;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class BusinessDomainException : Exception
    {
        public BusinessDomainException()
        { }

        public BusinessDomainException(string message)
            : base(message)
        { }

        public BusinessDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
