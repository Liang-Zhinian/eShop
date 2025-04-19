using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;

namespace Identity.API.Providers
{
    public interface IExternalAuthProvider
    {
        JObject GetUserInfo(string token);
    }
}
