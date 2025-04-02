﻿namespace Eva.eShop.Payment.API.IntegrationEvents.Events;
    
public record OrderStatusChangedToStockConfirmedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }

    public OrderStatusChangedToStockConfirmedIntegrationEvent(int orderId)
        => OrderId = orderId;
}
