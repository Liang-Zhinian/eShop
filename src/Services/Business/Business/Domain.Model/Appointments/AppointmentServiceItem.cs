using System;
using SaaSEqt.eShop.Business.Seedwork;

namespace SaaSEqt.eShop.Business.Domain.Model.Appointments
{
    public class AppointmentServiceItem
        : Entity
    {
        public AppointmentServiceItem()
        {
        }

        public AppointmentServiceItem(Guid id, string name, int defaultTimeLength, double price, double discount, Guid siteId)
        {
            ServiceItemId = id;
            Name = name;
            DefaultTimeLength = defaultTimeLength;
            Price = price;
            Discount = discount;
            SiteId = siteId;
        }

        public Guid ServiceItemId { get; private set; }

        public string Name { get; private set; }

        public int DefaultTimeLength { get; private set; }

        public double Price { get; private set; }

        public double Discount { get; private set; }

        public Guid SiteId { get; private set; }
    }
}
