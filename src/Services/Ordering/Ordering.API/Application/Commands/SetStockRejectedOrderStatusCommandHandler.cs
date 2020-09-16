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
    public class SetStockRejectedOrderStatusCommandHandler : IRequestHandler<SetStockRejectedOrderStatusCommand, bool>
    {        
        private readonly IOrderRepository _orderRepository;

        public SetStockRejectedOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// Stock service rejects the request
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetStockRejectedOrderStatusCommand command, CancellationToken cancellationToken)
        {
            // Simulate a work time for rejecting the stock
            await Task.Delay(10000);

            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if(orderToUpdate == null)
            {
                return false;
            }            

            orderToUpdate.SetCancelledStatusWhenStockIsRejected(command.OrderStockItems);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }


    // Use for Idempotency in Command process
    public class SetStockRejectedOrderStatusIdenfifiedCommandHandler : IdentifiedCommandHandler<SetStockRejectedOrderStatusCommand, bool>
    {
        public SetStockRejectedOrderStatusIdenfifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<SetStockRejectedOrderStatusCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}
