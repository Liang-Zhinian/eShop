using Basket.API.IntegrationEvents.Events;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Basket.API.Model;
using System;
using System.Threading.Tasks;

namespace Basket.API.IntegrationEvents.EventHandling
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;

        public OrderStartedIntegrationEventHandler(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            await _repository.DeleteBasketAsync(@event.UserId.ToString());
        }
    }
}



