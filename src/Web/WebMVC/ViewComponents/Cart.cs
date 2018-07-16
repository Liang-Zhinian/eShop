using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.WebMVC.ViewModels;
using SaaSEqt.eShop.WebMVC.ViewModels.CartViewModels;
using SaaSEqt.eShop.WebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polly.CircuitBreaker;

namespace SaaSEqt.eShop.WebMVC.ViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly IBasketService _cartSvc;

        public Cart(IBasketService cartSvc) => _cartSvc = cartSvc;

        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            var vm = new CartComponentViewModel();
            try
            {
                var itemsInCart = await ItemsInCartAsync(user);
                vm.ItemsCount = itemsInCart;
                return View(vm);
            }
            catch (BrokenCircuitException)
            {
                // Catch error when Basket.api is in circuit-opened mode                 
                ViewBag.IsBasketInoperative = true;
            }

            return View(vm);
        }
        private async Task<int> ItemsInCartAsync(ApplicationUser user)
        {
            var basket = await _cartSvc.GetBasket(user);
            return basket.Items.Count;
        }
    }
}
