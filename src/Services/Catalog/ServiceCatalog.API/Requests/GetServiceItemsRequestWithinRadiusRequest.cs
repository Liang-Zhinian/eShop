using System;
namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Request
{
    public class GetServiceItemsWithinRadiusRequest : BaseRequest
    {
        public GetServiceItemsWithinRadiusRequest()
        {
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public Guid LocationId { get; set; }
        public Guid ServiceItemId { get; set; }
        public string SearchText { get; set; }
        public string SortOption { get; set; }
    }
}
