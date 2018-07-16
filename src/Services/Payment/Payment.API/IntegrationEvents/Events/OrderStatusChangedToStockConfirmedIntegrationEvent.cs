namespace Payment.API.IntegrationEvents.Events
{
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

    public class OrderStatusChangedToStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderStatusChangedToStockConfirmedIntegrationEvent(int orderId)
            => OrderId = orderId;
    }
}