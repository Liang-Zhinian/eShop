namespace Eva.eShop.WebMVC.Services;

using Eva.eShop.WebMVC.ViewModels;

public interface IBasketService
{
    Task<Basket> GetBasket(ApplicationUser user);
    Task AddItemToBasket(ApplicationUser user, int productId);
    Task<Basket> UpdateBasket(Basket basket);
    Task Checkout(BasketDTO basket);
    Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
    Task<Order> GetOrderDraft(string basketId);
}
