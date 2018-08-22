
using MediatR;

namespace SaaSEqt.eShop.Business.Domain.Model.Appointments.Events
{
    public class AppointmentTotalsCalculatedDomainEvent
        : INotification
    {
        public decimal Total { get; set; }

        //public OrderLine[] Lines { get; set; }

        public bool IsFreeOfCharge { get; set; }
    }
}
