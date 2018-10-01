namespace SaaSEqt.eShop.Services.Sites.API
{
    public class SitesSettings
    {
        public string SiteLogoBaseUrl { get; set; }

        public string LocationPicBaseUrl { get; set; }

        public string LocationAdditionalPicBaseUrl { get; set; }

        public string ConnectionString { get; set; }

        public string EventBusConnection { get; set; }

        public bool UseCustomizationData { get; set; }

        public bool AzureStorageEnabled { get; set; }
    }
}
