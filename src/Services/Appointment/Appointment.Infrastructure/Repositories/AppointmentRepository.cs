using Microsoft.EntityFrameworkCore;
using AppointmentAggregate = SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate;
using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;
using Appointment.Domain.Exceptions;
using System;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Appointment.Infrastructure;

namespace SaaSEqt.eShop.Services.Appointment.Infrastructure.Repositories
{
    public class AppointmentRepository
        : AppointmentAggregate.IAppointmentRepository
    {
        private readonly AppointmentContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AppointmentRepository(AppointmentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AppointmentAggregate.Appointment Add(AppointmentAggregate.Appointment appointment)
        {
            return  _context.Appointments.Add(appointment).Entity;
               
        }

        public async Task<AppointmentAggregate.Appointment> GetAsync(Guid appointmentId)
        {
            var order = await _context.Appointments.FindAsync(appointmentId);
            if (order != null)
            {
                await _context.Entry(order)
                    .Collection(i => i.AppointmentServiceItems).LoadAsync();
                await _context.Entry(order)
                    .Reference(i => i.AppointmentStatus).LoadAsync();
                //await _context.Entry(order)
                //    .Reference(i => i.Address).LoadAsync();
            }

            return order;
        }

        public void Update(AppointmentAggregate.Appointment order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }
    }
}
