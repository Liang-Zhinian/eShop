using System;
using SaaSEqt.eShop.Services.Ordering.Domain.Seedwork;

namespace SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.AppointmentAggregate
{
    public class AppointmentServiceItem
        : Entity
    {
        public AppointmentServiceItem()
        {
        }

        public Guid ServiceItemId { get; private set; }

        public string Name { get; private set; }

        public int DefaultTimeLength { get; private set; }

        public double Price { get; private set; }

        public double Discount { get; private set; }

        public Guid SiteId { get; private set; }
    }
}
