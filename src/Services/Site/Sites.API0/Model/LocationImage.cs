using System;

namespace SaaSEqt.eShop.Services.Sites.API.Model
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

        public LocationImage(Guid siteId, Guid locationId, Guid id, string image)
        {
            Id = id;
            LocationId = locationId;
            Image = image;

            SiteId = siteId;
        }

        public void SetImage(string image){
            Image = image;
        }
    }
}
