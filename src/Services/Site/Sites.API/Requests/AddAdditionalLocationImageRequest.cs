using System;
using Microsoft.AspNetCore.Http;

namespace SaaSEqt.eShop.Services.Sites.API.Requests
{
    public class AddAdditionalLocationImageRequest
    {
        public AddAdditionalLocationImageRequest()
        {

        }

        public AddAdditionalLocationImageRequest(Guid siteId, Guid locationId, IFormFile image)
        {
            SiteId = siteId;
            LocationId = locationId;
            Image = image;
        }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public IFormFile Image { get; set; }
    }

    public class AddAdditionalLocationImageRequest_Test
    {
        public AddAdditionalLocationImageRequest_Test()
        {

        }

        public AddAdditionalLocationImageRequest_Test(Guid siteId, Guid locationId, string fileName = null)
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
