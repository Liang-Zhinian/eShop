﻿namespace Ordering.API.Application.IntegrationEvents.Events
{
    using Eva.BuildingBlocks.EventBus.Events;

    public class OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderStockConfirmedIntegrationEvent(int orderId) => OrderId = orderId;
    }
}