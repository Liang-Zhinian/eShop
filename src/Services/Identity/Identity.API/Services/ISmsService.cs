using System;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface ISmsService
    {
        Task<bool> SendAsync(string phoneNumber, string body);
    }
}
