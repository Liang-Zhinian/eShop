using System;
namespace SaaSEqt.eShop.Services.Branding.API.Model
{
    public class Version
    {
        public Version()
        {
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public int VersionNumber { get; set; }
        public string Language { get; set; }
    }
}
