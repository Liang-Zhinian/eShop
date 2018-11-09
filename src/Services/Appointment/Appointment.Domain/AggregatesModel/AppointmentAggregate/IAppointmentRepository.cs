using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;
using System;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IAppointmentRepository : IRepository<AppointmentAggregate.Appointment>
    {
        AppointmentAggregate.Appointment Add(AppointmentAggregate.Appointment appointment);
        
        void Update(AppointmentAggregate.Appointment appointment);

        Task<AppointmentAggregate.Appointment> GetAsync(Guid appointmentId);
    }
}
