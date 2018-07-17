using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
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
