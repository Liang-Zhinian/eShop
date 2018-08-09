using System;
namespace Registration.Api.Requests
{
    public class GetServiceItemsRequest : BaseRequest
    {
        public GetServiceItemsRequest()
        {
        }

        public Registration.Domain.ReadModel.Program[] ProgramIds { get; set; }
        public bool OnlineOnly { get; set; }
    }
}
