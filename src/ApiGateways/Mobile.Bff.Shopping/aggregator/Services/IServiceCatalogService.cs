using SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Services
{
    public interface IServiceCatalogService
    {
        Task<ServiceItem> GetCatalogItem(Guid id);
        Task<IEnumerable<ServiceItem>> GetCatalogItems(IEnumerable<Guid> ids);

        Task<ServiceCategory> GetCatalog(Guid id);
        Task<IEnumerable<ServiceCategory>> GetCatalogs(IEnumerable<Guid> ids);
    }
}
