using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Core.Models.Permissions;

namespace eShop.Core.Services.Permissions
{
    public interface IPermissionsService
    {
        Task<PermissionStatus> CheckPermissionStatusAsync(Permission permission);
        Task<Dictionary<Permission, PermissionStatus>> RequestPermissionsAsync(params Permission[] permissions);
    }
}
