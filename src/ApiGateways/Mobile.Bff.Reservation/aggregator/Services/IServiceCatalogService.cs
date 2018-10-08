using SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Services
{
    public interface IServiceCatalogService
    {
        Task<ServiceItem> GetCatalogItem(Guid id);
        Task<PaginatedItemsViewModel<ServiceItem>> GetSeriviceItemsWithinRadius(double latitude, double longitude, double radius, string searchText, int pageSize = 10, int pageIndex = 0);

        Task<ServiceCategory> GetCatalog(Guid id);
        Task<IEnumerable<ServiceCategory>> GetCatalogs(IEnumerable<Guid> ids);
    }
}
