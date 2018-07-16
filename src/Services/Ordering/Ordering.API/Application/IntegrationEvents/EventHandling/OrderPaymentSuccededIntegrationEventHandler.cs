namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
    using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
    using Ordering.API.Application.IntegrationEvents.Events;
    using System.Threading.Tasks;

    public class OrderPaymentSuccededIntegrationEventHandler : 
        IIntegrationEventHandler<OrderPaymentSuccededIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentSuccededIntegrationEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderPaymentSuccededIntegrationEvent @event)
        {
            // Simulate a work time for validating the payment
            await Task.Delay(10000);

            var orderToUpdate = await _orderRepository.GetAsync(@event.OrderId);

            orderToUpdate.SetPaidStatus();

            await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}