namespace SaaSEqt.eShop.Services.Ordering.API.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderQueries
    {
        Task<Order> GetOrderAsync(string id);

        Task<IEnumerable<OrderSummary>> GetOrdersAsync();

        Task<IEnumerable<CardType>> GetCardTypesAsync();
    }
}
