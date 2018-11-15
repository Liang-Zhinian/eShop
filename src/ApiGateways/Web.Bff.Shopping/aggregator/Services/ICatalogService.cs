using SaaSEqt.eShop.Web.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Web.Shopping.HttpAggregator.Services
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetCatalogItem(Guid id);
        Task<IEnumerable<CatalogItem>> GetCatalogItems(IEnumerable<Guid> ids);

        //Task<IEquatable<Sche>>
    }
}
