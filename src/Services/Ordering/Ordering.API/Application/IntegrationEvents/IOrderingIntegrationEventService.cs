using Eva.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Ordering.API.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync();
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
