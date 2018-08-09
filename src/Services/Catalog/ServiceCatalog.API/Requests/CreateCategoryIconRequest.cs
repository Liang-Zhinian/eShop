using System;
using Microsoft.AspNetCore.Http;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Request
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
        public string Type { get; set; }
        public int Order { get; set; }
    }
}
