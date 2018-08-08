using System;
using Microsoft.AspNetCore.Http;

namespace SaaSEqt.eShop.Services.Branding.API.Model
{
    public class CreateCategoryIconRequest
    {
        public CreateCategoryIconRequest()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        public string Language { get; set; }
    }
}
