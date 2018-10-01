using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Schedule.API.IntegrationEvents
{
    public interface IScheduleIntegrationEventService
    {
        Task SaveEventAndScheduleContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
