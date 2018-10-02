using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Sites;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Model;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.Sites
{
    public class SiteCreatedEventHandler : IIntegrationEventHandler<SiteCreatedEvent>
    {
        private readonly SitesDbContext _siteDbContext;
        public SiteCreatedEventHandler(SitesDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public async Task Handle(SiteCreatedEvent @event)
        {
            Site newSite = new Site(@event.SiteId, @event.TenantId, @event.Name, @event.Description, @event.Active);

            await _siteDbContext.AddAsync(newSite);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
