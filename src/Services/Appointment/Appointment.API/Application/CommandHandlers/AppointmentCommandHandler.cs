using System;
using System.Threading;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate;
using CqrsFramework.Commands;
using CqrsFramework.Domain;
using MediatR;
using SaaSEqt.eShop.Services.Appointment.Domain.Commands;

namespace Appointment.API.CommandHandlers
{
    public class AppointmentCommandHandler: CommandHandler, 
                                            ICommandHandler<MakeAnAppointmentCommand>,
                                            IRequestHandler<MakeAnAppointmentCommand, bool>
    {
        //private readonly ISession _session;
        public AppointmentCommandHandler(ISession session)
            : base(session)
        {
            //_session = session;
        }

        public async Task Handle(MakeAnAppointmentCommand message)
        {
            var appointment = new SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.Appointment(
                message.Id,
                message.AppointmentId,
                message.SiteId,
                message.StaffId,
                message.LocationId,
                message.StartDateTime,
                message.EndDateTime,
                message.ClientId,
                message.GenderPreference,
                message.Duration,
                message.StaffRequested,
                message.Notes
            );

            foreach (var item in message.AppointmentServiceItems)
            {
                appointment.AddAppointmentServiceItem(item.Id, item.Name, item.DefaultTimeLength, item.Price, item.Discount, item.SiteId);
            }

            foreach (var item in message.AppointmentResources)
            {
                appointment.AddAppointmentResource(item.Id, item.Name, item.SiteId);
            }

            await this.AddToSession(appointment);
            await this.CommitSession();
        }

        public async Task<bool> Handle(MakeAnAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate.Appointment(
                request.Id,
                request.AppointmentId,
                request.SiteId,
                request.StaffId,
                request.LocationId,
                request.StartDateTime,
                request.EndDateTime,
                request.ClientId,
                request.GenderPreference,
                request.Duration,
                request.StaffRequested,
                request.Notes//,
                //request.AppointmentServiceItems,
                //request.AppointmentResources
            );


            foreach (var item in request.AppointmentServiceItems)
            {
                appointment.AddAppointmentServiceItem(item.Id, item.Name, item.DefaultTimeLength, item.Price, item.Discount, item.SiteId);
            }

            foreach (var item in request.AppointmentResources)
            {
                appointment.AddAppointmentResource(item.Id, item.Name, item.SiteId);
            }

            await this.AddToSession(appointment);
            await this.CommitSession();

            return true;
        }
    }
}
