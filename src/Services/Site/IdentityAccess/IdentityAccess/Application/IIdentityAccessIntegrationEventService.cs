using System;
using System.Threading.Tasks;
using CqrsFramework.Events;

namespace SaaSEqt.IdentityAccess.Application
{
    public interface IIdentityAccessIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IEvent evt);
    }
}
