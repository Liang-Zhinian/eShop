
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SaaSEqt.eShop.Services.Ordering.API.Application.Commands.CreateOrderCommand;

namespace Ordering.API.Application.Models
{
    public static class BasketItemExtensions
    {
        public static IEnumerable<OrderItemDTO> ToOrderItemsDTO(this IEnumerable<BasketItem> basketItems)
        {
            foreach (var item in basketItems)
            {
                yield return item.ToOrderItemDTO();
            }
        }

        public static OrderItemDTO ToOrderItemDTO(this BasketItem item)
        {
            return new OrderItemDTO()
            {
                ProductId = Guid.TryParse(item.ProductId, out Guid id) ? id : Guid.Empty,
                ProductName = item.ProductName,
                PictureUrl = item.PictureUrl,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity,
                MerchantId = Guid.TryParse(item.MerchantId, out Guid merchantId) ? merchantId : Guid.Empty
            };
        }
    }
}
