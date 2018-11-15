namespace Ordering.API.Application.IntegrationEvents.Events
{
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
    using System;
    using System.Collections.Generic;

    public class OrderStockRejectedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public List<ConfirmedOrderStockItem> OrderStockItems { get; }

        public OrderStockRejectedIntegrationEvent(int orderId,
            List<ConfirmedOrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }

    public class ConfirmedOrderStockItem
    {
        public Guid ProductId { get; }
        public bool HasStock { get; }

        public ConfirmedOrderStockItem(Guid productId, bool hasStock)
        {
            ProductId = productId;
            HasStock = hasStock;
        }
    }
}