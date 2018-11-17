using System;
using Newtonsoft.Json.Linq;

namespace Identity.API.Providers
{
    public interface IExternalAuthProvider
    {
        JObject GetUserInfo(string accessToken);
    }
}
