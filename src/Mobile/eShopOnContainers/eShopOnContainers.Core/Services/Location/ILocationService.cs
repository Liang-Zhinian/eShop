using System.Threading.Tasks;

namespace eShop.Core.Services.Location
{    
    public interface ILocationService
    {
        Task UpdateUserLocation(eShop.Core.Models.Location.Location newLocReq, string token);
    }
}