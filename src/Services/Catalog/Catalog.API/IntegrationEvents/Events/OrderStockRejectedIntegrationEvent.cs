namespace SaaSEqt.eShop.Services.Catalog.API.IntegrationEvents.Events
{
    using System;
    using BuildingBlocks.EventBus.Events;
    using System.Collections.Generic;

    public class OrderStockRejectedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; }

        public List<ConfirmedOrderStockItem> OrderStockItems { get; }

        public OrderStockRejectedIntegrationEvent(Guid orderId,
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