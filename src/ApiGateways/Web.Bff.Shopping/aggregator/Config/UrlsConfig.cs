﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Web.Shopping.HttpAggregator.Config
{
    public class UrlsConfig
    {
        public class CatalogOperations
        {
            public static string GetItemById(string id) => $"/api/v1/catalog/items/{id}";
            public static string GetItemsById(IEnumerable<string> ids) => $"/api/v1/catalog/items?ids={string.Join(',', ids)}";
        }

        public class BasketOperations
        {
            public static string GetItemById(string id) => $"/api/v1/basket/{id}";
            public static string UpdateBasket() => "/api/v1/basket";
        }

        public class OrdersOperations
        {
            public static string GetOrderDraft() => "/api/v1/orders/draft";
        }

        public string Basket { get; set; }
        public string Catalog { get; set; }
        public string Orders { get; set; }
    }
}
