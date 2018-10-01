﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Data;
using SaaSEqt.eShop.Services.Sites.API.Model;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.Locations
{
    public class LocationPostalAddressChangedEventHandler : IIntegrationEventHandler<LocationAddressChangedEvent>
    {
        private readonly IHostingEnvironment _env;
        private readonly SitesDbContext _siteDbContext;

        public LocationPostalAddressChangedEventHandler(IHostingEnvironment env, SitesDbContext siteDbContext)
        {
            _env = env;
            _siteDbContext = siteDbContext;
        }

        public async Task Handle(LocationAddressChangedEvent @event)
        {
            Location existingLocation = await _siteDbContext.Locations.SingleOrDefaultAsync(y => y.Id.Equals(@event.LocationId));

            existingLocation.ChangeAddress(new Address(@event.StreetAddress,
                                                       @event.City,
                                                       @event.StateProvince,
                                                       @event.PostalCode,
                                                       @event.CountryCode));

            _siteDbContext.Locations.Update(existingLocation);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}