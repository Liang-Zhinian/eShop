using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.ServiceCatalogs;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Model.Catalog;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.ServiceCatalogs
{
    public class ServiceCategoryCreatedEventHandler : IIntegrationEventHandler<ServiceCategoryCreatedEvent>
    {
        private readonly SitesDbContext _siteDbContext;
        public ServiceCategoryCreatedEventHandler(SitesDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public async Task Handle(ServiceCategoryCreatedEvent @event)
        {
            ServiceCategory newServiceCategory = new ServiceCategory(@event.SiteId, 
                                                                     @event.ServiceCategoryId,
                                                                     @event.Name, 
                                                                     @event.Description, 
                                                                     @event.AllowOnlineScheduling,
                                                                     @event.ScheduleTypeValue);

            await _siteDbContext.ServiceCategories.AddAsync(newServiceCategory);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
