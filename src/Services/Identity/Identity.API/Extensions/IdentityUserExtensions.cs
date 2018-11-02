using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Identity.API.Models;

namespace SaaSEqt.eShop.Services.Identity.API.Extensions
{
    public static class IdentityUserExtensions
    {
        public static void FillUserAvatarUrl(this ApplicationUser user, string picBaseUrl, bool azureStorageEnabled)
        {
            user.AvatarImageUri = azureStorageEnabled
                ? picBaseUrl + user.AvatarImageFileName
                : picBaseUrl.Replace("[0]", user.Id.ToString());
        }
    }
}
