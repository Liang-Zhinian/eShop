using System;
namespace SaaSEqt.eShop.Services.Business.Api.Requests
{
    public class GetSitesRequest:BaseRequest
    {
        public GetSitesRequest()
        {
        }

        public string SearchText { get; set; }
        public Guid RelatedSiteID { get; set; }
        public bool ShowOnlyTotalWOD { get; set; }
    }
}
