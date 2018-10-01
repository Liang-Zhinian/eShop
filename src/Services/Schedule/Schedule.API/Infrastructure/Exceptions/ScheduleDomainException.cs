using System;

namespace Schedule.API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class ScheduleDomainException : Exception
    {
        public ScheduleDomainException()
        { }

        public ScheduleDomainException(string message)
            : base(message)
        { }

        public ScheduleDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
