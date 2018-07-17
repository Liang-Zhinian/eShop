using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;

namespace SaaSEqt.eShop.Services.Appointment.Infrastructure
{
    public class AppointmentContext : DbContext, IUnitOfWork
    {
        public AppointmentContext()
        {
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
