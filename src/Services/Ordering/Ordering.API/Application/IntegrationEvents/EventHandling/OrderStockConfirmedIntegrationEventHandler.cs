namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    using Eva.BuildingBlocks.EventBus.Abstractions;
    using System.Threading.Tasks;
    using Events;
    using Eva.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
    using MediatR;
    using System;
    using Ordering.API.Application.Commands;

    public class OrderStockConfirmedIntegrationEventHandler : 
        IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderStockConfirmedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(OrderStockConfirmedIntegrationEvent @event)
        {
            var command = new SetStockConfirmedOrderStatusCommand(@event.OrderId);
            await _mediator.Send(command);
        }
    }
}