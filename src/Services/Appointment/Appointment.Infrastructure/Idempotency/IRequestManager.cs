using System;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Appointment.Infrastructure.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(Guid id);

        Task CreateRequestForCommandAsync<T>(Guid id);
    }
}
