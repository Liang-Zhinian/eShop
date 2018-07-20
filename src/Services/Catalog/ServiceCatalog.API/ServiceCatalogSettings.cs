namespace SaaSEqt.eShop.Services.ServiceCatalog.API
{
    public class ServiceCatalogSettings
    {
        public string PicBaseUrl { get;set;}

        public string EventBusConnection { get; set; }

        public bool UseCustomizationData { get; set; }

	    public bool AzureStorageEnabled { get; set; }
    }
}
