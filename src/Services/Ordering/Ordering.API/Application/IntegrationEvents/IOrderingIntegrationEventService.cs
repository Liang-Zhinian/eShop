using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Ordering.API.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
