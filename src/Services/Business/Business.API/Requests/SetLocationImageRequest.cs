using System;
using Microsoft.AspNetCore.Http;

namespace Business.Api.Requests.Locations
{
    public class AddAdditionalLocationImageRequest
    {
        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public IFormFile Image { get; set; }
    }
}
