using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Sites.API.Model;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
using Sites.API.IntegrationEvents;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.Services
{
    public class BusinessService
    {
        private readonly SitesContext _sitesContext;
        private readonly ISitesIntegrationEventService _sitesIntegrationEventService;

        public BusinessService(
            SitesContext sitesContext,
            ISitesIntegrationEventService sitesIntegrationEventService)
        {
            this._sitesContext = sitesContext;
            _sitesIntegrationEventService = sitesIntegrationEventService;
        }

        #region site services

        public async Task<Site> ProvisionSite(Site site)
        {
            _sitesContext.Sites.Add(site);
            await _sitesContext.SaveChangesAsync();

            //SiteCreatedEvent siteCreatedEvent = new SiteCreatedEvent(site.TenantId,
            //                                                         site.Id,
            //                                                         site.Name,
            //                                                         site.Description,
            //                                                         site.Active,
            //                                                         site.ContactInformation.ContactName,
            //                                                         site.ContactInformation.PrimaryTelephone,
            //                                                         site.ContactInformation.SecondaryTelephone);


            //await _sitesIntegrationEventService.PublishThroughEventBusAsync(siteCreatedEvent);


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
            return await _sitesContext.Sites.SingleOrDefaultAsync(y=>y.Id.Equals(siteId));
        }

        #endregion

        #region Location services

        public async Task<Location> ProvisionLocation(Location location)
        {
            var existingSite = await FindExistingSite(location.SiteId); //await _eventStoreSession.Get<Site>(provisionLocationCommand.SiteId);

            var newLocation = existingSite.ProvisionLocation(location);

            await _sitesContext.SaveChangesAsync();

            //LocationCreatedEvent locationCreatedEvent = new LocationCreatedEvent(newLocation.SiteId,
            //                                                                     newLocation.Id,
            //                                                                     newLocation.Name,
            //                                                                     newLocation.Description);


            //await _sitesIntegrationEventService.PublishThroughEventBusAsync(locationCreatedEvent);


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

            await _sitesContext.SaveChangesAsync();

            //LocationContactInformationChangedEvent locationContactInformationChangedEvent = new LocationContactInformationChangedEvent(location.SiteId,
            //                                                                                                                           location.Id,
            //                                                                                                                           location.ContactInformation.ContactName,
            //                                                                                                                           location.ContactInformation.PrimaryTelephone,
            //                                                                                                                           location.ContactInformation.SecondaryTelephone,
            //                                                                                                                           location.ContactInformation.EmailAddress);


            //await _sitesIntegrationEventService.PublishThroughEventBusAsync(locationContactInformationChangedEvent);


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

            //await _sitesContext.SaveChangesAsync();

            //LocationAddressChangedEvent locationAddressChangedEvent = new LocationAddressChangedEvent(location.SiteId,
            //                                                                                              location.Id,
            //                                                                                              location.Address.Street,
            //                                                                                              location.Address.City,
            //                                                                                              location.Address.StateProvince,
            //                                                                                              location.Address.ZipCode,
            //                                                                                              location.Address.Country);
            //await _sitesIntegrationEventService.PublishThroughEventBusAsync(locationAddressChangedEvent);

            return location;
        }

        public async Task<Location> SetLocationGeolocation(Guid siteId, Guid locationId, double latitude, double longitude)
        {
            var location = await FindExistingLocation(siteId, locationId);

            Geolocation geolocation = new Geolocation(latitude, longitude);

            location.ChangeGeolocation(geolocation);

            await _sitesContext.SaveChangesAsync();


            //LocationGeolocationChangedEvent locationGeolocationChangedEvent = new LocationGeolocationChangedEvent(location.SiteId,
            //                                                                                                      location.Id,
            //                                                                                                      location.Geolocation.Latitude,
            //                                                                                                      location.Geolocation.Longitude);
            //await _sitesIntegrationEventService.PublishThroughEventBusAsync(locationGeolocationChangedEvent);

            return location;
        }

        public async Task UpdateLocationImage(Guid siteId, Guid locationId, string image)
        {
            //Guid siteId, Guid locationId, byte[] image
            var location = await FindExistingLocation(siteId, locationId);

            location.ChangeImage(image);

            await _sitesContext.SaveChangesAsync();

            //LocationImageChangedEvent locationImageChangedEvent = new LocationImageChangedEvent(location.SiteId,
            //                                                                                    location.Id,
            //                                                                                    location.Image);


            //await _sitesIntegrationEventService.PublishThroughEventBusAsync(locationImageChangedEvent);

        }

        public async Task<LocationImage> AddOrUpdateAdditionalLocationImage(Guid siteId, Guid locationId, Guid? imageId, string image)
        {
            var location = await FindExistingLocation(siteId, locationId);
            LocationImage locationImage = null;
            if (!imageId.HasValue)
            {
                locationImage = new LocationImage(siteId, locationId, Guid.NewGuid(), image);
                location.AddAdditionalImage(locationImage);

                //AdditionalLocationImageCreatedEvent additionalLocationImageCreatedEvent =
                //    new AdditionalLocationImageCreatedEvent(location.SiteId,
                //                                          location.Id,
                //                                          locationImage.Id,
                //                                            location.Image);
                //await _sitesIntegrationEventService.PublishThroughEventBusAsync(additionalLocationImageCreatedEvent);
            } else{
                locationImage = location.AdditionalLocationImages.SingleOrDefault(y => y.Id.Equals(imageId));
                if (locationImage == null) throw new Exception("AdditionalLocationImage not found.");

                locationImage.SetImage(image);

                //AdditionalLocationImageUpdatedEvent additionalLocationImageUpdatedEvent =
                //    new AdditionalLocationImageUpdatedEvent(location.SiteId,
                //                                          location.Id,
                //                                          locationImage.Id,
                //                                            location.Image);
                //await _sitesIntegrationEventService.PublishThroughEventBusAsync(additionalLocationImageUpdatedEvent);
            }

            await _sitesContext.SaveChangesAsync();
            return locationImage;

        }

        public IQueryable<Location> FindLocations(Guid siteId)
        {
            var locations = _sitesContext.Locations
                                                   .Include(y => y.AdditionalLocationImages)
                                                   .Where(y => y.SiteId.Equals(siteId));
            return locations;
        }

        public async Task<Location> FindExistingLocation(Guid siteId, Guid locationId)
        {
            var location = await _sitesContext.Locations
                                                   .Include(y=>y.AdditionalLocationImages)
                                                   .SingleOrDefaultAsync(y => y.SiteId.Equals(siteId) &&
                                                                               y.Id.Equals(locationId));
            return location;
        }

        public async Task<IEnumerable<Location>> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText) {
            var root = await _sitesContext.Locations.Include(y => y.AdditionalLocationImages).ToListAsync();
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
