using System;
namespace SaaSEqt.eShop.Business.API.Requests
{
    public class GetBusinessLocationsWithinRadiusRequest: BaseRequest
    {
        public GetBusinessLocationsWithinRadiusRequest()
        {
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public Guid LocationId { get; set; }
        public string SearchText { get; set; }
        public string SortOption { get; set; }
    }
}
