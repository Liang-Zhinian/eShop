using SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Services
{
    public interface IOrderApiClient
    {
        Task<OrderData> GetOrderDraftFromBasket(BasketData basket);
    }
}
