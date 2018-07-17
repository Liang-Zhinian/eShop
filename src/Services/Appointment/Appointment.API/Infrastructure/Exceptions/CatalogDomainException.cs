using System;

namespace Appointment.API.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class AppointmentDomainException : Exception
    {
		public AppointmentDomainException()
        { }

		public AppointmentDomainException(string message)
            : base(message)
        { }

		public AppointmentDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
