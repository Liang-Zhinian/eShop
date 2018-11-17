using System;
using System.Linq;
using Identity.API.Infrastucture.Repositories;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace Identity.API.Providers
{
    public class WechatAuthProvider<TUser> : IWechatAuthProvider where TUser : IdentityUser, new()
    {
        private readonly IProviderRepository _providerRepository;

        public WechatAuthProvider()
        {
        }

        public Provider Provider
        {
            get
            {
                return _providerRepository.Get().FirstOrDefault(y => y.Name.ToLower() == "wechat");
            }
        }

        public JObject GetUserInfo(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
