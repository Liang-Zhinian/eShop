using System;

namespace SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Models
{
    public class LocationImage
    {
        public Guid Id { get; set; }

        public string Image { get; set; }

        public string ImageUri { get; set; }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }
    }
}
