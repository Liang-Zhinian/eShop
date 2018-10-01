using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Sites.API.IntegrationEvents
{
    public interface ISitesIntegrationEventService
    {
        Task SaveEventAndSitesContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
