﻿using Eva.eShop.Web.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eva.eShop.Web.Shopping.HttpAggregator.Services
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetCatalogItem(int id);
        Task<IEnumerable<CatalogItem>> GetCatalogItems(IEnumerable<int> ids);
    }
}
