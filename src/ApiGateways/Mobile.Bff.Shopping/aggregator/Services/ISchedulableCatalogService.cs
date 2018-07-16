using SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Services
{
    public interface ISchedulableCatalogService
    {
        Task<SchedulableCatalogItem> GetCatalogItem(Guid id);
        Task<IEnumerable<SchedulableCatalogItem>> GetCatalogItems(IEnumerable<Guid> ids);

        Task<SchedulableCatalogType> GetCatalog(Guid id);
        Task<IEnumerable<SchedulableCatalogType>> GetCatalogs(IEnumerable<Guid> ids);
    }
}
