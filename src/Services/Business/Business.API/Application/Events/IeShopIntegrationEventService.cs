using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events
{
    public interface IeShopIntegrationEventService
    {
        //Task SaveEventAndBusinessDbContextChangesAsync(IEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
