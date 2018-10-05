using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Sites.API.Model;

namespace SaaSEqt.eShop.Services.Sites.API.Extensions
{
    public static class StaffExtensions
    {
        public static void FillStaffImageUrl(this Staff staff, string picBaseUrl, bool azureStorageEnabled)
        {
            if (staff.Image == null) return;

            staff.ImageUri = azureStorageEnabled
                ? picBaseUrl + staff.ImageUri
                : picBaseUrl.Replace("[0]", staff.SiteId.ToString()).Replace("[1]", staff.Id.ToString());
        }
    }
}
