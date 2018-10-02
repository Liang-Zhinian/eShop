using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SaaSEqt.eShop.Services.Appointment.Domain.Events;
using CqrsFramework.Domain;
using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;

namespace SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate
{
    public class Appointment
        : AggregateRoot
    {
        #region ctor

        protected Appointment()
        {
            this.AppointmentServiceItems = new ObservableCollection<AppointmentServiceItem>();
            this.AppointmentResources = new ObservableCollection<AppointmentResource>();
            this._isDraft = false;
        }

        public Appointment(
            Guid id,
            Guid appointmentId,
            Guid siteId,
            Guid staffId,
            Guid locationId,
            DateTime startDateTime,
            DateTime endDateTime,
            Guid clientId,
            string genderPreference,
            int duration,
            bool staffRequested,
            string notes
        ):this()
        {
            //this.Id = id;
            //this.AppointmentId = appointmentId;
            //this.SiteId = siteId;
            //this.StaffId = staffId;
            //this.LocationId = locationId;
            //this.StartDateTime = startDateTime;
            //this.EndDateTime = endDateTime;
            //this.ClientId = clientId;
            //this.GenderPreference = genderPreference;
            //this.Duration = duration;
            //this.StaffRequested = staffRequested;
            //this.Notes = notes;
            //this.AppointmentServiceItems = appointmentServiceItems;
            //this.AppointmentResources = resources;

            ApplyChange(new AppointmentPlacedDomainEvent(
                id,
                appointmentId,
                siteId,
                locationId,
                staffId,
                startDateTime,
                endDateTime,
                clientId,
                genderPreference,
                duration,
                staffRequested,
                notes,
                this.AppointmentServiceItems,
                this.AppointmentResources
            ));
        }

        public void AddAppointmentResource(Guid id, string name, Guid siteId)
        {
            this.AppointmentResources.Add(new AppointmentResource(id, name, siteId));
        }

        public void AddAppointmentServiceItem(Guid id, string name, int defaultTimeLength, double price, double discount, Guid siteId)
        {
            this.AppointmentServiceItems.Add(new AppointmentServiceItem(id, name, defaultTimeLength, price, discount, siteId));
        }

        public static Appointment NewDraft()
        {
            var appointment = new Appointment();
            appointment._isDraft = true;
            return appointment;
        }

        #endregion

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

        #region ordering cycle commands

        public decimal GetTotal()
        {
            throw new NotImplementedException();
        }

        public void SetAwaitingValidationStatus()
        {
            throw new NotImplementedException();
        }

        public void SetCancelledStatus()
        {
            throw new NotImplementedException();
        }

        public void SetPaidStatus()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region apply changes

        public void Apply(AppointmentPlacedDomainEvent @event){

            this.Id = @event.Id;
            this.AppointmentId = @event.AppointmentId;
            this.SiteId = @event.SiteId;
            this.StaffId = @event.StaffId;
            this.LocationId = @event.LocationId;
            this.StartDateTime = @event.StartDateTime;
            this.EndDateTime = @event.EndDateTime;
            this.ClientId = @event.ClientId;
            this.GenderPreference = @event.GenderPreference;
            this.Duration = @event.Duration;
            this.StaffRequested = @event.StaffRequested;
            this.Notes = @event.Notes;
            this.AppointmentServiceItems = @event.AppointmentServiceItems;
            this.AppointmentResources = @event.AppointmentResources;
        }

        #endregion
    }
}
