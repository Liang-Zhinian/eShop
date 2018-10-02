using System;
namespace Business.API.Requests.Locations
{
    public class SetLocationGeolocationRequest
    {
        public SetLocationGeolocationRequest()
        {

        }

        public SetLocationGeolocationRequest(Guid siteId, Guid id, double latitude, double longitude)
        {
            SiteId = siteId;
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
