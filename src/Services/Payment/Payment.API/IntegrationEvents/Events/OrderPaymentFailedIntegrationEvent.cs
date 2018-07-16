namespace Payment.API.IntegrationEvents.Events
{
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderPaymentFailedIntegrationEvent(int orderId) => OrderId = orderId;
    }
}