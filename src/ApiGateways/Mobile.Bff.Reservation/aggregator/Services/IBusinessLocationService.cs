using SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Services
{
    public interface IBusinessLocationService
    {
        Task<Location> GetBusinessLocation(Guid siteId, Guid locationId);
        Task<IEnumerable<Location>> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText, int pageSize = 10, int pageIndex = 0);

    }
}
