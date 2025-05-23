﻿namespace Eva.eShop.Services.Ordering.API.Application.IntegrationEvents.Events;    

public record OrderStockRejectedIntegrationEvent : IntegrationEvent
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

public record ConfirmedOrderStockItem
{
    public int ProductId { get; }
    public bool HasStock { get; }

    public ConfirmedOrderStockItem(int productId, bool hasStock)
    {
        ProductId = productId;
        HasStock = hasStock;
    }
}
