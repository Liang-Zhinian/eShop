﻿namespace Eva.eShop.Services.Ordering.API.Application.Commands;
using Eva.eShop.Services.Ordering.API.Application.Models;

public class CreateOrderDraftCommand : IRequest<OrderDraftDTO>
{

    public string BuyerId { get; private set; }

    public IEnumerable<BasketItem> Items { get; private set; }

    public CreateOrderDraftCommand(string buyerId, IEnumerable<BasketItem> items)
    {
        BuyerId = buyerId;
        Items = items;
    }
}
