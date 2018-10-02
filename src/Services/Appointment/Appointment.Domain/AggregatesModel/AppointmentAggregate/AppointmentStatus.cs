using System;
using System.Collections.Generic;
using System.Linq;
using SaaSEqt.eShop.Services.Appointment.Domain.SeedWork;

namespace SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate
{
    public class AppointmentStatus : Enumeration
    {
        public static AppointmentStatus Booked = new AppointmentStatus(1, nameof(Booked).ToLowerInvariant());
        public static AppointmentStatus Completed = new AppointmentStatus(2, nameof(Completed).ToLowerInvariant());
        public static AppointmentStatus Confirmed = new AppointmentStatus(3, nameof(Confirmed).ToLowerInvariant());
        public static AppointmentStatus Arrived = new AppointmentStatus(4, nameof(Arrived).ToLowerInvariant());
        public static AppointmentStatus NoShow = new AppointmentStatus(5, nameof(NoShow).ToLowerInvariant());
        public static AppointmentStatus Cancelled = new AppointmentStatus(6, nameof(Cancelled).ToLowerInvariant());

        protected AppointmentStatus()
        {
        }

        public AppointmentStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<AppointmentStatus> List() =>
        new[] { Booked, Completed, Confirmed, Arrived, NoShow, Cancelled };

        public static AppointmentStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for ScheduleType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static AppointmentStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for AppointmentStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
