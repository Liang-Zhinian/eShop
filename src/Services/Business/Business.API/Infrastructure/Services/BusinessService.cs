using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Business.Infrastructure.Data;
using SaaSEqt.eShop.Services.Business.Domain.Model.Security;
using SaaSEqt.eShop.Services.Business.Infrastructure;
using SaaSEqt.eShop.Services.Business.API.Application.Events;
using SaaSEqt.eShop.Services.Business.API.Application.Events.Sites;
using SaaSEqt.eShop.Services.Business.API.Application.Events.Locations;

namespace SaaSEqt.eShop.Services.Business.API.Infrastructure.Services
{
    public class BusinessService
    {
        private readonly BusinessDbContext _businessDbContext;
        private readonly IeShopIntegrationEventService _eShopIntegrationEventService;

        public BusinessService(
            BusinessDbContext businessDbContext,
            IeShopIntegrationEventService eShopIntegrationEventService)
        {
            this._businessDbContext = businessDbContext;
            _eShopIntegrationEventService = eShopIntegrationEventService;
        }

        #region site services

        public async Task<Site> ProvisionSite(Site site)
        {
            _businessDbContext.Sites.Add(site);
            //await _businessDbContext.SaveChangesAsync();

            SiteCreatedEvent siteCreatedEvent = new SiteCreatedEvent(site.TenantId,
                                                                     site.Id,
                                                                     site.Name,
                                                                     site.Description,
                                                                     site.Active,
                                                                     site.ContactInformation.ContactName,
                                                                     site.ContactInformation.PrimaryTelephone,
                                                                     site.ContactInformation.SecondaryTelephone);


            await _eShopIntegrationEventService.PublishThroughEventBusAsync(siteCreatedEvent);


            return site;
        }

        public async Task ActivateSite(Guid siteId)
        {
            throw new NotImplementedException();
        }

        public Task DeactivateSite(Guid siteId)
        {
            throw new NotImplementedException();
        }

        public async Task<Site> FindExistingSite(Guid siteId){
            return await _businessDbContext.Sites.SingleOrDefaultAsync(y=>y.Id.Equals(siteId));
        }

        #endregion

        #region Location services

        public async Task<Location> ProvisionLocation(Location location)
        {
            var existingSite = await FindExistingSite(location.SiteId); //await _eventStoreSession.Get<Site>(provisionLocationCommand.SiteId);

            var newLocation = existingSite.ProvisionLocation(location);

            //await _businessDbContext.SaveChangesAsync();

            LocationCreatedEvent locationCreatedEvent = new LocationCreatedEvent(newLocation.SiteId,
                                                                                 newLocation.Id,
                                                                                 newLocation.Name,
                                                                                 newLocation.Description);


            await _eShopIntegrationEventService.PublishThroughEventBusAsync(locationCreatedEvent);


            return location;
        }

        public async Task<Location> UpdateLocationInformation(Guid siteId,
                                                              Guid locationId,
                                                              string contactName,
                                                              string emailAddress,
                                                              string primaryTelephone,
                                                              string secondaryTelephone
                                                             ){
            var location = await FindExistingLocation(siteId, locationId); //await _eventStoreSession.Get<Location>(locationId);

            ContactInformation contactInformation = new ContactInformation(contactName,
                                                                           primaryTelephone,
                                                                           secondaryTelephone,
                                                                           emailAddress);

            location.ChangeContactInformation(contactInformation);

            //await _businessDbContext.SaveChangesAsync();

            LocationContactInformationChangedEvent locationContactInformationChangedEvent = new LocationContactInformationChangedEvent(location.SiteId,
                                                                                                                                       location.Id,
                                                                                                                                       location.ContactInformation.ContactName,
                                                                                                                                       location.ContactInformation.PrimaryTelephone,
                                                                                                                                       location.ContactInformation.SecondaryTelephone,
                                                                                                                                       location.ContactInformation.EmailAddress);


            await _eShopIntegrationEventService.PublishThroughEventBusAsync(locationContactInformationChangedEvent);


            return location;
        }

        public async Task<Location> SetLocationAddress(Guid siteId,
                                             Guid locationId,
                                             string street,
                             string city,
                             string stateProvince,
                                             string zipCode,
                                             string country)
        {
            var location = await FindExistingLocation(siteId, locationId); //await _eventStoreSession.Get<Location>(locationId);

            Address address = new Address(street,
                              city,
                              stateProvince,
                              zipCode,
                              country);

            location.ChangeAddress(address);

            //await _businessDbContext.SaveChangesAsync();

            LocationAddressChangedEvent locationAddressChangedEvent = new LocationAddressChangedEvent(location.SiteId,
                                                                                                          location.Id,
                                                                                                          location.Address.Street,
                                                                                                          location.Address.City,
                                                                                                          location.Address.StateProvince,
                                                                                                          location.Address.ZipCode,
                                                                                                          location.Address.Country);
            await _eShopIntegrationEventService.PublishThroughEventBusAsync(locationAddressChangedEvent);

            return location;
        }

        public async Task<Location> SetLocationGeolocation(Guid siteId, Guid locationId, double latitude, double longitude)
        {
            var location = await FindExistingLocation(siteId, locationId);

            Geolocation geolocation = new Geolocation(latitude, longitude);

            location.ChangeGeolocation(geolocation);

            //await _businessDbContext.SaveChangesAsync();


            LocationGeolocationChangedEvent locationGeolocationChangedEvent = new LocationGeolocationChangedEvent(location.SiteId,
                                                                                                                  location.Id,
                                                                                                                  location.Geolocation.Latitude,
                                                                                                                  location.Geolocation.Longitude);
            await _eShopIntegrationEventService.PublishThroughEventBusAsync(locationGeolocationChangedEvent);

            return location;
        }

        public async Task UpdateLocationImage(Guid siteId, Guid locationId, string image)
        {
            //Guid siteId, Guid locationId, byte[] image
            var location = await FindExistingLocation(siteId, locationId);

            location.ChangeImage(image);

            //await _businessDbContext.SaveChangesAsync();

            LocationImageChangedEvent locationImageChangedEvent = new LocationImageChangedEvent(location.SiteId,
                                                                                                location.Id,
                                                                                                location.Image);


            await _eShopIntegrationEventService.PublishThroughEventBusAsync(locationImageChangedEvent);

        }

        public async Task<LocationImage> AddAdditionalLocationImage(Guid siteId, Guid locationId, string image)
        {
            var location = await FindExistingLocation(siteId, locationId);

            var locationImage = new LocationImage(siteId, locationId, image);
            location.AddAdditionalImage(locationImage);

            //await _businessDbContext.SaveChangesAsync();

            AdditionalLocationImageCreatedEvent additionalLocationImageCreatedEvent =
                new AdditionalLocationImageCreatedEvent(location.SiteId,
                                                      location.Id,
                                                      locationImage.Id,
                                                        location.Image);
            await _eShopIntegrationEventService.PublishThroughEventBusAsync(additionalLocationImageCreatedEvent);


            return locationImage;

        }

        public async Task<Location> FindExistingLocation(Guid siteId, Guid locationId)
        {
            var location = await _businessDbContext.Locations
                                                   .Include(y=>y.AdditionalLocationImages)
                                                   .SingleOrDefaultAsync(y => y.SiteId.Equals(siteId) &&
                                                                               y.Id.Equals(locationId));
            return location;
        }

        public async Task<IEnumerable<Location>> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText) {
            var root = await _businessDbContext.Locations.Include(y => y.AdditionalLocationImages).ToListAsync();
            IList<Location> list = new List<Location>();
            foreach (var item in root)
            {
                if (DistanceHelper.Distance(latitude, longitude, item.Geolocation.Latitude, item.Geolocation.Longitude, 'K')<= radius){
                    list.Add(item);
                }
            }
            return list;
        }

        #endregion
    }
}
