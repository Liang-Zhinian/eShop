using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Application.IntegrationEvents.Events;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Application.IntegrationEvents.EventHandling
{
    public class ServiceItemCreatedEventHandler : IIntegrationEventHandler<ServiceItemCreatedEvent>
    {
        private readonly CatalogContext _context;
        public ServiceItemCreatedEventHandler(CatalogContext context)
        {
            _context = context;
        }

        public async Task Handle(ServiceItemCreatedEvent @event)
        {
            ServiceItem newServiceItem = new ServiceItem(@event.SiteId, 
                                                         @event.ServiceItemId,
                                                         @event.Name, 
                                                         @event.Description, 
                                                         @event.DefaultTimeLength,
                                                         @event.Price,
                                                         @event.ServiceCategoryId,
                                                         @event.IndustryStandardCategoryName,
                                                         @event.IndustryStandardSubcategoryName);

            await _context.ServiceItems.AddAsync(newServiceItem);
            await _context.SaveChangesAsync();
        }
    }
}
