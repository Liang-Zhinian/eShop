using eShop.Core.Models.User;
using System.Threading.Tasks;

namespace eShop.Core.Services.User
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfoAsync(string authToken);
    }
}
