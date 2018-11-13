using System;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public interface ISmsService
    {
        Task<bool> SendAsync(string phoneNumber, string body);
    }
}
