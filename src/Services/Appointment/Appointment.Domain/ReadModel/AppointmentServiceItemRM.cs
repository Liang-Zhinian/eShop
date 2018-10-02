using System;
using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;

namespace Appointment.Domain.ReadModel
{
    public class AppointmentServiceItemRM
    {
        public AppointmentServiceItemRM()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int DefaultTimeLength { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public Guid SiteId { get; set; }
    }
}
