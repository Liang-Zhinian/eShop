using Basket.API.IntegrationEvents.Events;
using Eva.BuildingBlocks.EventBus.Abstractions;
using Eva.eShop.Services.Basket.API;
using Eva.eShop.Services.Basket.API.Model;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Basket.API.IntegrationEvents.EventHandling
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<OrderStartedIntegrationEventHandler> _logger;

        public OrderStartedIntegrationEventHandler(
            IBasketRepository repository,
            ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventIdContext", @event.Id))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppShortName} - ({@IntegrationEvent})", @event.Id, Program.AppShortName, @event);

                await _repository.DeleteBasketAsync(@event.UserId.ToString());
            }
        }
    }
}



