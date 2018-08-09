using System;
namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Request
{
    public class GetProgramsRequest:BaseRequest
    {
        public GetProgramsRequest()
        {
        }

        public string /* or class ScheduleType */ ScheduleType { get; set; }
        public bool OnlineOnly { get; set; }
    }
}
