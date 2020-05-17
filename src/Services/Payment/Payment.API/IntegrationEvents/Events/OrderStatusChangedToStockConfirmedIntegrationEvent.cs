namespace Payment.API.IntegrationEvents.Events
{
    using Eva.BuildingBlocks.EventBus.Events;

    public class OrderStatusChangedToStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderStatusChangedToStockConfirmedIntegrationEvent(int orderId)
            => OrderId = orderId;
    }
}