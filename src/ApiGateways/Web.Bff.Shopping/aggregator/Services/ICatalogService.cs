using SaaSEqt.eShop.Web.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Web.Shopping.HttpAggregator.Services
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetCatalogItem(string id);
        Task<IEnumerable<CatalogItem>> GetCatalogItems(IEnumerable<string> ids);

        //Task<IEquatable<Sche>>
    }
}
