using System;

namespace SaaSEqt.eShop.Services.Sites.API.Model.Appointments
{
    public class AppointmentResource
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
