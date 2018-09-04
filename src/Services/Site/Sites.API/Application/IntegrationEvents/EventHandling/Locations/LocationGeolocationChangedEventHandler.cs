using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Model.Security;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.Locations
{
    public class LocationGeolocationChangedEventHandler : IIntegrationEventHandler<LocationGeolocationChangedEvent>
    {
        private readonly IHostingEnvironment _env;
        private readonly SitesDbContext _siteDbContext;

        public LocationGeolocationChangedEventHandler(IHostingEnvironment env, SitesDbContext siteDbContext)
        {
            _env = env;
            _siteDbContext = siteDbContext;
        }

        public async Task Handle(LocationGeolocationChangedEvent @event)
        {
            Location existingLocation = await _siteDbContext.Locations.SingleOrDefaultAsync(y => y.Id.Equals(@event.LocationId));

            double latitude = @event.Latitude.HasValue ? @event.Latitude.Value : 0;
            double longitude = @event.Longitude.HasValue ? @event.Longitude.Value : 0;

            Geolocation geolocation = new Geolocation(latitude, longitude);

            existingLocation.ChangeGeolocation(geolocation);

            _siteDbContext.Locations.Update(existingLocation);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
