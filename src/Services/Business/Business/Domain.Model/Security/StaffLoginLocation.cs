using System;

namespace SaaSEqt.eShop.Business.Domain.Model.Security
{
    public class StaffLoginLocation
    {
        public Guid Id { get; private set; }

        public Guid StaffId { get; private set; }
        public virtual Staff Staff { get; private set; }

        public Guid LocationId { get; private set; }
        public virtual Location Location { get; private set; }

        public Guid SiteId { get; private set; }
        public virtual Site Site { get; private set; }

        public StaffLoginLocation(Guid siteId, Guid staffId, Guid locationId)
        {
            Id = Guid.NewGuid();
            StaffId = staffId;
            LocationId = locationId;

            this.SiteId = siteId;
        }
    }
}
