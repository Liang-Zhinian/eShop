using System;
namespace SaaSEqt.eShop.Mobile.Shopping.Reservation.HttpAggregator.Models
{
    public class ServiceItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Guid SiteId { get; set; }
    }
}
