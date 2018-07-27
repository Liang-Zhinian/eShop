namespace SaaSEqt.eShop.Services.Business.API
{
    public class BusinessSettings
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
