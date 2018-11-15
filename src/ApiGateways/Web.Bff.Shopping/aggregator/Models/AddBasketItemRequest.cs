using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Web.Shopping.HttpAggregator.Models
{
    public class AddBasketItemRequest
    {
        public Guid CatalogItemId { get; set; }
        public string BasketId { get; set; }

        public int Quantity { get; set; }

        public AddBasketItemRequest()
        {
            Quantity = 1;
        }
    }
}
