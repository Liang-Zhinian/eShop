using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using Identity.API.Infrastucture.Repositories;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace Identity.API.Providers
{
    public class WechatAuthProvider<TUser> : IWechatAuthProvider where TUser : IdentityUser, new()
    {
        private readonly IProviderRepository _providerRepository;
        private readonly HttpClient _httpClient;

        public WechatAuthProvider(
            IProviderRepository providerRepository,
            HttpClient httpClient
        )
        {
            _providerRepository = providerRepository;
            _httpClient = httpClient;
        }

        public Provider Provider => _providerRepository.Get()
                                          .FirstOrDefault(y => y.Name.ToLower() == "wechat");

        public JObject GetUserInfo(string accessToken)
        {
            if (Provider == null) throw new ArgumentNullException(nameof(Provider));

            var openid = "";
            var request = $"{Provider.UserInfoEndPoint}?access_token={accessToken}&openid={openid}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("cache-control", "no-cache");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "token " + accessToken);

            try
            {
                var result = _httpClient.GetAsync(Provider.UserInfoEndPoint).Result;
                if (result.IsSuccessStatusCode)
                {
                    var infoObject = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                    return infoObject;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
