using System;
using System.Collections.Generic;

namespace SaaSEqt.eShop.Services.Business.API.Requests
{
    public class BaseRequest
    {
        public BaseRequest()
        {
        }

        public SourceCredentials SourceCredentials { get; set; }

        public UserCredentials UserCredentials { get; set; }

        public int PageSize { get; set; }

        public int CurrentPageIndex { get; set; }

        public ICollection<string> Fields { get; set; }
    }

    public class SourceCredentials{
        public string SourceName { get; set; }
        public string Password { get; set; }
        public ICollection<Guid>  SiteIDs { get; set; }
    }

    public class UserCredentials { 
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Guid> SiteIDs { get; set; }
    }

    public class StaffCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Guid> SiteIDs { get; set; }
    }
}
