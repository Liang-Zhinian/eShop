using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.ServiceCatalogs;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Model.Catalog;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.ServiceCatalogs
{
    public class ServiceItemCreatedEventHandler : IIntegrationEventHandler<ServiceItemCreatedEvent>
    {
        private readonly SitesDbContext _siteDbContext;
        public ServiceItemCreatedEventHandler(SitesDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
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

            await _siteDbContext.ServiceItems.AddAsync(newServiceItem);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
