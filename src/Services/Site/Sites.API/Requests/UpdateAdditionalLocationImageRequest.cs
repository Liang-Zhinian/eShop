using System;
using Microsoft.AspNetCore.Http;

namespace SaaSEqt.eShop.Services.Sites.API.Requests
{
    public class UpdateAdditionalLocationImageRequest
    {
        public UpdateAdditionalLocationImageRequest()
        {

        }

        public UpdateAdditionalLocationImageRequest(Guid siteId, Guid locationId, Guid imageId, IFormFile image)
        {
            SiteId = siteId;
            LocationId = locationId;
            ImageId = imageId;
            Image = image;
        }

        public Guid ImageId { get; set; }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public IFormFile Image { get; set; }
    }
}
