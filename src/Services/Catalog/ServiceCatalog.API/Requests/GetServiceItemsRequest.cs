using System;
namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Request
{
    public class GetServiceItemsRequest : BaseRequest
    {
        public GetServiceItemsRequest()
        {
        }

        public Guid[] ProgramIds { get; set; }
        public bool OnlineOnly { get; set; }
    }
}
