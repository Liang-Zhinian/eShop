using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents
{
    public interface IIdentityAccessIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task SaveEventAndContextChangesAsync(IntegrationEvent evt);
    }
}
