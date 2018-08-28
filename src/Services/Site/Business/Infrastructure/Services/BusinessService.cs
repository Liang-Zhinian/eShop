using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Business.Infrastructure.Data;
using SaaSEqt.eShop.Business.Domain.Model.Security;

namespace SaaSEqt.eShop.Business.Infrastructure.Services
{
    public class BusinessService
    {
        private readonly BusinessDbContext _businessDbContext;

        public BusinessService(
            BusinessDbContext businessDbContext)
        {
            this._businessDbContext = businessDbContext;
        }

        #region site services

        public async Task<Site> ProvisionSite(Site site)
        {
            _businessDbContext.Sites.Add(site);
            await _businessDbContext.SaveChangesAsync();

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

            location = existingSite.ProvisionLocation(location);

            await _businessDbContext.SaveChangesAsync();

            return location;
        }


        public async Task SetLocationAddress(Guid siteId, 
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

            await _businessDbContext.SaveChangesAsync();

            //await _eventStoreSession.Add<Location>(location);
            //await _eventStoreSession.Commit();

            //_locationRepository.Update(location);
            //_locationRepository.UnitOfWork.Commit();

            //await _businessIntegrationEventService
            //    .PublishThroughEventBusAsync(new LocationAddressChangedEvent(location.Id,
            //                                                                 location.SiteId,
            //                                                                 address.StreetAddress,
            //                                            address.StreetAddress2,
            //                                                                 address.City,
            //                                            address.StateProvince,
            //                                                                 address.PostalCode,
            //                                            address.CountryCode)
            //);
        }

        public async Task SetLocationGeolocation(Guid siteId, Guid locationId, double latitude, double longitude)
        {
            var location = await FindExistingLocation(siteId, locationId);

            Geolocation geolocation = new Geolocation(latitude, longitude);

            location.ChangeGeolocation(geolocation);

            await _businessDbContext.SaveChangesAsync();


            //await _eventStoreSession.Add<Location>(location);
            //await _eventStoreSession.Commit();

            //_locationRepository.Update(location);
            //_locationRepository.UnitOfWork.Commit();

            //await _businessIntegrationEventService
                //.PublishThroughEventBusAsync(new LocationGeolocationChangedEvent(
                    //locationId,
                    //siteId,
                    //latitude,
                    //longitude));
        }

        public async Task UpdateLocationImage(Guid siteId, Guid locationId, string image)
        {
            //Guid siteId, Guid locationId, byte[] image
            var location = await FindExistingLocation(siteId, locationId);

            location.ChangeImage(image);

            await _businessDbContext.SaveChangesAsync();

            //await _eventStoreSession.Add<Location>(location);
            //await _eventStoreSession.Commit();

            //_locationRepository.Update(location);
            //_locationRepository.UnitOfWork.Commit();

            //await _businessIntegrationEventService
                //.PublishThroughEventBusAsync(new LocationImageChangedEvent(location.Id,
                                                                           //location.SiteId,
                                                                           //updateLocationImageCommand.Image));
        }

        public async Task AddAdditionalLocationImage(Guid siteId, Guid locationId, string image)
        {
            var location = await FindExistingLocation(siteId, locationId);

            var locationImage = new LocationImage(siteId, locationId, image);
            location.AddAdditionalImage(locationImage);

            await _businessDbContext.SaveChangesAsync();

            //await _eventStoreSession.Add<Location>(location);
            //await _eventStoreSession.Commit();

            //_locationRepository.Update(location);
            //_locationRepository.UnitOfWork.Commit();


            //await _businessIntegrationEventService
                //.PublishThroughEventBusAsync(new AdditionalLocationImageCreatedEvent(location.Id,
                                                                           //location.SiteId,
                                                                                     //image));
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
