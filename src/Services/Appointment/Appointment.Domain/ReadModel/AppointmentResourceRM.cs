using System;
using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;

namespace Appointment.Domain.ReadModel
{
    public class AppointmentResourceRM
    {
        public AppointmentResourceRM()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Guid SiteId { get; private set; }
    }
}
