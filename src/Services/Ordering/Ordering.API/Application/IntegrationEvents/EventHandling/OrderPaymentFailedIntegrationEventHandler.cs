﻿namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    using MediatR;
    using Eva.BuildingBlocks.EventBus.Abstractions;
    using Eva.BuildingBlocks.EventBus.Extensions;
    using Eva.eShop.Services.Ordering.API;
    using Eva.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
    using Microsoft.Extensions.Logging;
    using Ordering.API.Application.Behaviors;
    using Ordering.API.Application.Commands;
    using Ordering.API.Application.IntegrationEvents.Events;
    using Serilog.Context;
    using System.Threading.Tasks;
    using System;

    public class OrderPaymentFailedIntegrationEventHandler : 
        IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderPaymentFailedIntegrationEventHandler> _logger;

        public OrderPaymentFailedIntegrationEventHandler(
            IMediator mediator,
            ILogger<OrderPaymentFailedIntegrationEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderPaymentFailedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                var command = new CancelOrderCommand(@event.OrderId);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    command.GetGenericTypeName(),
                    nameof(command.OrderNumber),
                    command.OrderNumber,
                    command);

                await _mediator.Send(command);
            }
        }
    }
}
