using System;
using MediatR;

namespace SaaSEqt.eShop.Services.Appointment.Domain.Events
{
    public class AppointmentReservationCompletedDomainEvent
        : INotification
    {
        public DateTime ReservationExpiration { get; set; }

        //public IEnumerable<SeatQuantity> Seats { get; set; }
    }
}
