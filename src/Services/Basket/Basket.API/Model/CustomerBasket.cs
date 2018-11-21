using System.Collections.Generic;

namespace SaaSEqt.eShop.Services.Basket.API.Model
{
    public class CustomerBasket
    {
        public string BuyerId { get;  set; }
        public List<BasketItem> Items { get; set; } 
        public Dictionary<string, List<BasketItem>> OrganizedItems { get; set; }

        public CustomerBasket(string customerId)
        {
            BuyerId = customerId;
            Items = new List<BasketItem>();
            OrganizedItems = new Dictionary<string, List<BasketItem>>();
        }
    }
}
