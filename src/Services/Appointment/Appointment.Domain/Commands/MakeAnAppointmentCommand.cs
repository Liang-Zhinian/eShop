using System;
using System.Collections.Generic;
using Appointment.Domain.ReadModel;
using CqrsFramework.Commands;
using MediatR;
using SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate;

namespace SaaSEqt.eShop.Services.Appointment.Domain.Commands
{
    public class MakeAnAppointmentCommand : BaseCommand, ICommand, IRequest<bool>
    {
        public MakeAnAppointmentCommand()
        {
        }

        public MakeAnAppointmentCommand(Guid commandId, Guid appointmentId, Guid siteId, Guid locationId, Guid staffId, DateTime startDateTime, DateTime endDateTime, Guid clientId, string genderPreference, int duration, bool staffRequested, string notes, ICollection<AppointmentServiceItemRM> appointmentServiceItems, ICollection<AppointmentResourceRM> appointmentResources)
        {
            Id = commandId;
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

        public Guid Id {get;set;}
        public Guid AppointmentId {get;set;}
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
        public ICollection<AppointmentServiceItemRM> AppointmentServiceItems { get; set; }
        public ICollection<AppointmentResourceRM> AppointmentResources { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
