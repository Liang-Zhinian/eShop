using System;
using SaaSEqt.eShop.Business.Seedwork;

namespace SaaSEqt.eShop.Business.Domain.Model.Appointments
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
