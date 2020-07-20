using System;

namespace PaymentServiceProvider.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class PaymentServiceProviderDomainException : Exception
    {
        public PaymentServiceProviderDomainException()
        { }

        public PaymentServiceProviderDomainException(string message)
            : base(message)
        { }

        public PaymentServiceProviderDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
