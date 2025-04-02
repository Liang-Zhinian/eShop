namespace Eva.eShop.Web.Shopping.HttpAggregator.Services;

public interface IOrderingService
{
    Task<OrderData> GetOrderDraftAsync(BasketData basketData);
}
