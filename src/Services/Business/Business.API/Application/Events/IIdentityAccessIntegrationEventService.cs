using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events
{
    public interface IIdentityAccessIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task SaveEventAndContextChangesAsync(IntegrationEvent evt);
    }
}
