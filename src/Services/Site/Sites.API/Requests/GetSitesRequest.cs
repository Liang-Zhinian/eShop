﻿using System;
namespace SaaSEqt.eShop.Services.Sites.API.Requests
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
