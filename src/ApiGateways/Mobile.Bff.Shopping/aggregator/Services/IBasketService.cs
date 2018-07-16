using SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Mobile.Shopping.HttpAggregator.Services
{
    public interface IBasketService
    {
        Task<BasketData> GetById(string id);
        Task Update(BasketData currentBasket);

    }
}
