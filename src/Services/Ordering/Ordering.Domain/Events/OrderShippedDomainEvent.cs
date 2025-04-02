namespace Eva.eShop.Services.Ordering.Domain.Events;

public class OrderShippedDomainEvent : INotification
{
    public Order Order { get; }

    public OrderShippedDomainEvent(Order order)
    {
        Order = order;
    }
}
