﻿using MediatR;
using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.Domain.Events
{
    public class OrderShippedDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderShippedDomainEvent(Order order)
        {
            Order = order;           
        }
    }
}
