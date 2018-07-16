using System;
using SaaSEqt.eShop.Services.Ordering.Domain.Seedwork;

namespace SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.AppointmentAggregate
{
    public class AppointmentResource
        : Entity
    {
        public AppointmentResource()
        {
        }

        public Guid ResourceId { get; private set; }

        public Guid ResourceName { get; private set; }
    }
}
