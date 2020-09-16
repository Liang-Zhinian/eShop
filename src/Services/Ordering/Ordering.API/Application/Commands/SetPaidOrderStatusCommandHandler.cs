﻿using MediatR;
using Eva.eShop.Services.Ordering.API.Application.Commands;
using Eva.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using Eva.eShop.Services.Ordering.Infrastructure.Idempotency;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Application.Commands
{
    // Regular CommandHandler
    public class SetPaidOrderStatusCommandHandler : IRequestHandler<SetPaidOrderStatusCommand, bool>
    {        
        private readonly IOrderRepository _orderRepository;

        public SetPaidOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// Shipment service confirms the payment
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetPaidOrderStatusCommand command, CancellationToken cancellationToken)
        {
            // Simulate a work time for validating the payment
            await Task.Delay(10000);

            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if(orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetPaidStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }


    // Use for Idempotency in Command process
    public class SetPaidIdentifiedOrderStatusCommandHandler : IdentifiedCommandHandler<SetPaidOrderStatusCommand, bool>
    {
        public SetPaidIdentifiedOrderStatusCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<SetPaidOrderStatusCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}
