using System;
using Microsoft.AspNetCore.Http;

namespace Business.API.Requests.Locations
{
    public class SetLocationImageRequest
    {
        public SetLocationImageRequest()
        {

        }

        public SetLocationImageRequest(Guid siteId, Guid locationId, IFormFile image)
        {
            SiteId = siteId;
            LocationId = locationId;
            Image = image;
        }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public IFormFile Image { get; set; }
    }

    public class SetLocationImageRequest_Test
    {
        public SetLocationImageRequest_Test()
        {

        }

        public SetLocationImageRequest_Test(Guid siteId, Guid locationId, string fileName = null)
        {
            SiteId = siteId;
            LocationId = locationId;
            FileName = fileName;
        }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public string FileName { get; set; }
    }
}
