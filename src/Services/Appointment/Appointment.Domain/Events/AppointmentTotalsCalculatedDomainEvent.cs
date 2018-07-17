
using MediatR;

namespace SaaSEqt.eShop.Services.Appointment.Domain.Events
{
    public class AppointmentTotalsCalculatedDomainEvent
        : INotification
    {
        public decimal Total { get; set; }

        //public OrderLine[] Lines { get; set; }

        public bool IsFreeOfCharge { get; set; }
    }
}
