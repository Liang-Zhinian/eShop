namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
    using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
    using Ordering.API.Application.IntegrationEvents.Events;
    using System.Threading.Tasks;

    public class OrderPaymentFailedIntegrationEventHandler : 
        IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderPaymentFailedIntegrationEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderPaymentFailedIntegrationEvent @event)
        {
            var orderToUpdate = await _orderRepository.GetAsync(@event.OrderId);

            orderToUpdate.SetCancelledStatus();

            await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
