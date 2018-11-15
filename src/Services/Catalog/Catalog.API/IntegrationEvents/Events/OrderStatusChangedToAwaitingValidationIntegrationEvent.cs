namespace SaaSEqt.eShop.Services.Catalog.API.IntegrationEvents.Events
{
    using BuildingBlocks.EventBus.Events;
    using System;
    using System.Collections.Generic;

    public class OrderStatusChangedToAwaitingValidationIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToAwaitingValidationIntegrationEvent(Guid orderId,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }

    public class OrderStockItem
    {
        public Guid ProductId { get; }
        public int Units { get; }

        public OrderStockItem(Guid productId, int units)
        {
            ProductId = productId;
            Units = units;
        }
    }
}