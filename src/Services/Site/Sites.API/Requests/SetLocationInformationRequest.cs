using System;
namespace SaaSEqt.eShop.Services.Sites.API.Requests
{
    public class SetLocationInformationRequest
    {
       
        public SetLocationInformationRequest()
        {
        }

        public SetLocationInformationRequest(Guid siteId, Guid locationId, string contactName, string emailAddress, string primaryTelephone, string secondaryTelephone)
        {
            SiteId = siteId;
            LocationId = locationId;
            ContactName = contactName;
            EmailAddress = emailAddress;
            PrimaryTelephone = primaryTelephone;
            SecondaryTelephone = secondaryTelephone;
        }

        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        public string ContactName { get; set; }

        public string EmailAddress { get; set; }

        public string PrimaryTelephone { get; set; }

        public string SecondaryTelephone { get; set; }
    }
}
