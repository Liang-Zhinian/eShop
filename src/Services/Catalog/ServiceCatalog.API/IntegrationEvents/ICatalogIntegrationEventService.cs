using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace ServiceCatalog.API.IntegrationEvents
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
