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
        public IList<AppointmentServiceItemRM> AppointmentServiceItems { get; set; }
        public IList<AppointmentResourceRM> AppointmentResources { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
