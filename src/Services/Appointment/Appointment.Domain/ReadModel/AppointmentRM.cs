using System;
using System.Collections.Generic;

namespace Appointment.Domain.ReadModel
{
    public class AppointmentRM
    {
        public AppointmentRM()
        {
        }

        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Guid StaffId { get; set; }
        public Guid LocationId { get; set; }
        public Guid SiteId { get; set; }
        public string GenderPreference { get; set; }
        public int Duration { get; set; }
        public int AppointmentStatusId { get; set; }
        public AppointmentStatusRM AppointmentStatus { get; set; }
        public string Notes { get; set; }
        public bool StaffRequested { get; set; }
        public Guid ClientId { get; set; }
        public bool FirstAppointment { get; set; }
        public Guid AggregateID { get; set; }

        public ICollection<AppointmentServiceItemRM> AppointmentServiceItems { get; private set; }

        public ICollection<AppointmentResourceRM> AppointmentResources { get; private set; }
    }
}
