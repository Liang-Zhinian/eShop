using MediatR;
using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using Ordering.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static SaaSEqt.eShop.Services.Ordering.API.Application.Commands.CreateOrderCommand;

namespace SaaSEqt.eShop.Services.Ordering.API.Application.Commands
{
    public class CreateOrderDraftCommand :  IRequest<OrderDraftDTO>
    {
       
        public string BuyerId { get; private set; }

        public IEnumerable<BasketItem> Items { get; private set; }

        public CreateOrderDraftCommand(string buyerId, IEnumerable<BasketItem> items)
        {
            BuyerId = buyerId;
            Items = items;
        }
    }

}
