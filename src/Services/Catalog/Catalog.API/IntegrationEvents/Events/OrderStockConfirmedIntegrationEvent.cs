namespace SaaSEqt.eShop.Services.Catalog.API.IntegrationEvents.Events
{
    using System;
    using BuildingBlocks.EventBus.Events;

    public class OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; }

        public OrderStockConfirmedIntegrationEvent(Guid orderId) => OrderId = orderId;
    }
}