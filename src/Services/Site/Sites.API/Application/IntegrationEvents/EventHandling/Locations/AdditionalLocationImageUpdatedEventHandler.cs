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
    public class AdditionalLocationImageUpdatedEventEventHandler : IIntegrationEventHandler<AdditionalLocationImageUpdatedEvent>
    {
        private readonly IHostingEnvironment _env;
        private readonly SitesDbContext _siteDbContext;

        public AdditionalLocationImageUpdatedEventEventHandler(IHostingEnvironment env, SitesDbContext siteDbContext)
        {
            _env = env;
            _siteDbContext = siteDbContext;
        }

        public async Task Handle(AdditionalLocationImageCreatedEvent @event)
        {
            Location existingLocation = await _siteDbContext.Locations
                                                            .Include(y=>y.AdditionalLocationImages)
                                                            .SingleOrDefaultAsync(y => y.Id.Equals(@event.LocationId));

            LocationImage locationImage = existingLocation.AdditionalLocationImages
                                                          .SingleOrDefault(y => y.SiteId == @event.SiteId
                                                                           && y.LocationId == @event.LocationId
                                                                           && y.Id == @event.ImageId);
            locationImage.SetImage(@event.FileName);

            _siteDbContext.Locations.Update(existingLocation);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
