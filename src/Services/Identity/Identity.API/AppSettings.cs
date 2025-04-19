namespace Eva.eShop.Services.Identity.API
{
    public class AppSettings
    {
        public string MvcClient { get; set; }

        public bool UseCustomizationData { get; set; }

        public string PicBaseUrl { get; set; }
        public bool AzureStorageEnabled { get; set; }
    }
}
