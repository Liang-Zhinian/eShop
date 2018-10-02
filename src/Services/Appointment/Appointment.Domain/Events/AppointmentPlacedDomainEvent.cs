using System;
using System.Collections.Generic;
using CqrsFramework.Events;
using MediatR;
using SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate;

namespace SaaSEqt.eShop.Services.Appointment.Domain.Events
{
    public class AppointmentPlacedDomainEvent
        : INotification, IEvent
    {
        public AppointmentPlacedDomainEvent()
        {
        }

        public AppointmentPlacedDomainEvent(Guid id, Guid appointmentId, Guid siteId, Guid locationId, Guid staffId, DateTime startDateTime, DateTime endDateTime, Guid clientId, string genderPreference, int duration, bool staffRequested, string notes, ICollection<AppointmentServiceItem> appointmentServiceItems, ICollection<AppointmentResource> appointmentResources)
        {
            Id = id;
            AppointmentId = appointmentId;
            SiteId = siteId;
            LocationId = locationId;
            StaffId = staffId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            ClientId = clientId;
            GenderPreference = genderPreference;
            Duration = duration;
            StaffRequested = staffRequested;
            Notes = notes;
            AppointmentServiceItems = appointmentServiceItems;
            AppointmentResources = appointmentResources;
        }

        public Guid AppointmentId { get; set; }
        public Guid SiteId { get; set; }
        public Guid LocationId { get; set; }
        public Guid StaffId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Guid ClientId { get; set; }
        public string GenderPreference { get; set; }
        public int Duration { get; set; }
        public bool StaffRequested { get; set; }
        public string Notes { get; set; }
        public ICollection<AppointmentServiceItem> AppointmentServiceItems { get; set; }
        public ICollection<AppointmentResource> AppointmentResources { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
