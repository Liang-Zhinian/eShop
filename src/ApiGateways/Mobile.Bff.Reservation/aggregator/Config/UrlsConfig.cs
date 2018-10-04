using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Config
{
    public class UrlsConfig
    {
        public class CatalogOperations
        {
            public static string GetItemById(int id) => $"/api/v1/catalog/items/{id}";
            public static string GetItemsById(IEnumerable<int> ids) => $"/api/v1/catalog/items?ids={string.Join(',', ids)}";
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
        public string Business { get; set; }


        // Schedulable Catalog
        public class ServiceCatalogOperations
        {
            public static string GetItemById(Guid id) => $"/api/v1/servicecatalog/serviceitems/{id}";
            public static string GetItemsById(IEnumerable<Guid> ids) => $"/api/v1/servicecatalog/serviceitems?ids={string.Join(',', ids)}";
            public static string GetCatalogById(Guid id) => $"/api/v1/servicecatalog/servicecategories/{id}";
            public static string GetCatalogsById(IEnumerable<Guid> ids) => $"/api/v1/servicecatalog/servicecategories?ids={string.Join(',', ids)}";
        }

        // Business
        public class BusinessLocationOperations
        {
            public static string GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText, int pageSize = 10, int pageIndex = 0) => $"/api/v1/sites/GetBusinessLocationsWithinRadius?latitude={latitude}&longitude={longitude}&radius={radius}&searchText={searchText}&pageSize={pageSize}&pageIndex={pageIndex}";
            public static string GetBusinessLocation(Guid siteId, Guid locationId) => $"/api/v1/sites/{siteId}/locations/{locationId}";
        }
    }
}
