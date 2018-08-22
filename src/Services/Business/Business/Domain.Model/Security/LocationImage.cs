using System;

namespace SaaSEqt.eShop.Business.Domain.Model.Security
{
    public class LocationImage
    {
        public Guid Id { get; private set; }
        public string Image { get; private set; }
        public string ImageUri { get; set; }

        public Guid LocationId { get; private set; }
        public virtual Location Location { get; private set; }

        public Guid SiteId { get; private set; }
        public virtual Site Site { get; private set; }

        private LocationImage()
        {

        }

        public LocationImage(Guid siteId, Guid locationId, string image)
        {
            Id = Guid.NewGuid();
            LocationId = locationId;
            Image = image;

            SiteId = siteId;
        }
    }
}
