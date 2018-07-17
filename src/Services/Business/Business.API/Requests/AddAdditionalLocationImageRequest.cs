using System;
using Microsoft.AspNetCore.Http;

namespace SaaSEqt.eShop.Services.Business.Api.Requests
{
    public class SetLocationImageRequest
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public IFormFile Image { get; set; }
    }
}
