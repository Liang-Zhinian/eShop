using Identity.API.Models;

namespace Identity.API.Configuration
{
    public class ProviderDataSource
    {
        public static IEnumerable<Provider> GetProviders()
        {
            return new List<Provider>
            {
                new Provider{
                    ProviderId = 1,
                    Name = "Wechat",
                    UserInfoEndPoint = "https://api.weixin.qq.com/sns/userinfo"
                }
            };
        }
    }
}
