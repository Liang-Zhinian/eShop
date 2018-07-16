using System;
using SaaSEqt.eShop.Services.Appointment.Domain.Seedwork;

namespace SaaSEqt.eShop.Services.Appointment.Domain.AggregatesModel.AppointmentAggregate
{
    public class AppointmentResource
        : Entity
    {
        protected AppointmentResource()
        {
        }

        public AppointmentResource(Guid id, string name, Guid siteId)
        {
            ResourceId = id;
            ResourceName = name;
            SiteId = siteId;
        }

        public Guid ResourceId { get; private set; }

        public string ResourceName { get; private set; }

        public Guid SiteId { get; private set; }
    }
}
