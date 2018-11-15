using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Web.Shopping.HttpAggregator.Models
{
    public class CatalogItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }


        public string PictureUri { get; set; }

        public Guid MerchantId { get; set; }

    }
}
