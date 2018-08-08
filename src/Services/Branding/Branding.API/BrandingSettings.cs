namespace SaaSEqt.eShop.Services.Branding.API
{
    public class BrandingSettings
    {
        public string PicBaseUrl { get;set;}

        public string EventBusConnection { get; set; }

        public bool UseCustomizationData { get; set; }

	    public bool AzureStorageEnabled { get; set; }
    }
}
