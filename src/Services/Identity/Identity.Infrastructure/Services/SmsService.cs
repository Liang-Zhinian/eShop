using System;
using System.Threading.Tasks;
using Aliyun.Acs.Dysmsapi.Model.V20170525;

namespace Identity.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        public SmsService()
        {
        }

        public async Task<bool> SendAsync(string phoneNumber, string body)
        {
            var response = await SmsSender.Send(phoneNumber, body);
            if (response.Code == "OK")
                return true;

            return false;
        }
    }
}
