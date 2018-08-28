using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Model.Security;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.Locations
{
    public class LocationCreatedEventHandler : IIntegrationEventHandler<Events.Locations.LocationCreatedEvent>
    {
        private readonly SitesDbContext _siteDbContext;
		public LocationCreatedEventHandler(SitesDbContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

        public async Task Handle(Events.Locations.LocationCreatedEvent @event)
        {
            Location newLocation = new Location(@event.LocationId, @event.SiteId, @event.Name, @event.Description, @event.Active);

            await _siteDbContext.Locations.AddAsync(newLocation);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
