namespace SaaSEqt.eShop.Services.ServiceCatalog.API.IntegrationEvents.Events
{
    using System.Collections.Generic;
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

    public class OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToPaidIntegrationEvent(int orderId,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }
}