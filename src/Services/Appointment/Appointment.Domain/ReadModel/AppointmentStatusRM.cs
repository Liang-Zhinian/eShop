using System;
using System.Collections.Generic;
using System.Linq;
using SaaSEqt.eShop.Services.Appointment.Domain.SeedWork;

namespace Appointment.Domain.ReadModel
{
    public class AppointmentStatusRM : Enumeration
    {
        public static AppointmentStatusRM Booked = new AppointmentStatusRM(1, nameof(Booked).ToLowerInvariant());
        public static AppointmentStatusRM Completed = new AppointmentStatusRM(2, nameof(Completed).ToLowerInvariant());
        public static AppointmentStatusRM Confirmed = new AppointmentStatusRM(3, nameof(Confirmed).ToLowerInvariant());
        public static AppointmentStatusRM Arrived = new AppointmentStatusRM(4, nameof(Arrived).ToLowerInvariant());
        public static AppointmentStatusRM NoShow = new AppointmentStatusRM(5, nameof(NoShow).ToLowerInvariant());
        public static AppointmentStatusRM Cancelled = new AppointmentStatusRM(6, nameof(Cancelled).ToLowerInvariant());

        protected AppointmentStatusRM()
        {
        }

        public AppointmentStatusRM(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<AppointmentStatusRM> List() =>
        new[] { Booked, Completed, Confirmed, Arrived, NoShow, Cancelled };

        public static AppointmentStatusRM FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for ScheduleType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static AppointmentStatusRM From(int id)
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
