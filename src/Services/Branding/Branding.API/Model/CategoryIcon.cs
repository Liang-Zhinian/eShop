using System;
namespace SaaSEqt.eShop.Services.Branding.API.Model
{
    public class CategoryIcon
    {
        public CategoryIcon()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUri { get; set; }
        public string IconFileName { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        public int VersionNumber { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Language { get; set; }
    }
}
