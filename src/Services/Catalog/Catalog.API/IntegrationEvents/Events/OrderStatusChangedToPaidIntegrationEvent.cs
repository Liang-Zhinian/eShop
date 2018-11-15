namespace SaaSEqt.eShop.Services.Catalog.API.IntegrationEvents.Events
{
    using System;
    using System.Collections.Generic;
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

    public class OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToPaidIntegrationEvent(Guid orderId,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }
}