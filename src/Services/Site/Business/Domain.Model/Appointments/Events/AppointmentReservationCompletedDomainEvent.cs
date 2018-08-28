using System;
using MediatR;

namespace SaaSEqt.eShop.Business.Domain.Model.Appointments.Events
{
    public class AppointmentReservationCompletedDomainEvent
        : INotification
    {
        public DateTime ReservationExpiration { get; set; }

        //public IEnumerable<SeatQuantity> Seats { get; set; }
    }
}
