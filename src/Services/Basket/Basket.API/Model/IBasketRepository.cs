﻿namespace Eva.eShop.Services.Basket.API.Model;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string customerId);
    IEnumerable<string> GetUsers();
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string id);
}

