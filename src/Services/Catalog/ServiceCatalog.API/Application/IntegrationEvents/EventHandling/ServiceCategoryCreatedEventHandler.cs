using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Application.IntegrationEvents.Events;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Application.IntegrationEvents.EventHandling
{
    public class ServiceCategoryCreatedEventHandler : IIntegrationEventHandler<ServiceCategoryCreatedEvent>
    {
        private readonly CatalogContext _context;
        public ServiceCategoryCreatedEventHandler(CatalogContext context)
        {
            _context = context;
        }

        public async Task Handle(ServiceCategoryCreatedEvent @event)
        {
            ServiceCategory newServiceCategory = new ServiceCategory(@event.SiteId, 
                                                                     @event.ServiceCategoryId,
                                                                     @event.Name, 
                                                                     @event.Description, 
                                                                     @event.AllowOnlineScheduling,
                                                                     @event.ScheduleTypeValue);

            await _context.ServiceCategories.AddAsync(newServiceCategory);
            await _context.SaveChangesAsync();
        }
    }
}
