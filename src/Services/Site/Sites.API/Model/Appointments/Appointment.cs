using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaaSEqt.eShop.Services.Sites.API.Model.Appointments
{
    public class Appointment
    {
        #region properties

        public Guid AppointmentId { get; private set; }

        // Draft orders have this set to true. Currently we don't check anywhere the draft status of an Order, but we could do it if needed
        private bool _isDraft;

        public DateTime OrderDate { get; private set; }

        /// Start time
        public DateTime StartDateTime { get; private set; }

        /// End time.
        public DateTime EndDateTime { get; private set; }

        ///// Staff, teacher, or trainer
        public Guid StaffId { get; private set; }

        /// The session type of the schedule
        //public Guid ServiceItemId { get; private set; }

        /// The location of the schedule
        public Guid LocationId { get; private set; }

        public Guid SiteId { get; private set; }

        /// Prefered gender of appointment
        public string GenderPreference { get; private set; }

        /// Duration of appointment. Only used to change appointment default duration for add.
        public int Duration { get; private set; }

        /// If a user has Complementary and Alternative Medicine features enabled. This will allow a Provider ID to be assigned to an appointment.
        //public string ProviderID { get; private set; }

        /// The status of this appointment.
        private int _appointmentStatusId;
        public AppointmentStatus AppointmentStatus { get; private set; }

        /// The appointment notes.
        public string Notes { get; private set; }

        /// Whether the staff member was requested specifically by the client.
        public bool StaffRequested { get; private set; }

        /// The client booked for this appointment.
        public Guid ClientId { get; private set; }
        //public Client Client { get; private set; }

        /// Whether this is the client's first appointment at the site.
        public bool FirstAppointment { get; private set; }

        /// The service on the client's account that is paying for this appointment.
        //public PurchasedService ClientService { get; private set; }

        public ICollection<AppointmentServiceItem> AppointmentServiceItems { get; private set; }

        /// The resources this appointment is using.
        public ICollection<AppointmentResource> AppointmentResources { get; private set; }

        #endregion

    }
}
