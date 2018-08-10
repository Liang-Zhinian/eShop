namespace SaaSEqt.eShop.Services.IndustryStandardCategory.API
{
    public class CategorySettings
    {
        public string PicBaseUrl { get;set;}

        public string EventBusConnection { get; set; }

        public bool UseCustomizationData { get; set; }
	public bool AzureStorageEnabled { get; set; }
    }
}
