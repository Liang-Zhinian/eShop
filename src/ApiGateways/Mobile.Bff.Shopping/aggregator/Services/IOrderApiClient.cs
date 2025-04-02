namespace Eva.eShop.Mobile.Shopping.HttpAggregator.Services;

public interface IOrderApiClient
{
    Task<OrderData> GetOrderDraftFromBasketAsync(BasketData basket);
}
