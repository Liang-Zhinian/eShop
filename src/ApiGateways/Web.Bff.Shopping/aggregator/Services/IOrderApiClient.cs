namespace Eva.eShop.Web.Shopping.HttpAggregator.Services;

public interface IOrderApiClient
{
    Task<OrderData> GetOrderDraftFromBasketAsync(BasketData basket);
}
