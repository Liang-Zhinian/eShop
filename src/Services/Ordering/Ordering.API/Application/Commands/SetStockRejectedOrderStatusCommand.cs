﻿namespace Eva.eShop.Services.Ordering.API.Application.Commands;

public class SetStockRejectedOrderStatusCommand : IRequest<bool>
{

    [DataMember]
    public int OrderNumber { get; private set; }

    [DataMember]
    public List<int> OrderStockItems { get; private set; }

    public SetStockRejectedOrderStatusCommand(int orderNumber, List<int> orderStockItems)
    {
        OrderNumber = orderNumber;
        OrderStockItems = orderStockItems;
    }
}
