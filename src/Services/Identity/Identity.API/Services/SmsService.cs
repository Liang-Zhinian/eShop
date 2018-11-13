using System;
using System.Threading.Tasks;
            
namespace Identity.API.Services
{
    public class SmsService : ISmsService
    {
        public SmsService()
        {
        }

        public Task<bool> SendAsync(string phoneNumber, string body)
        {
            return Task.FromResult(true);
        }
    }
}
