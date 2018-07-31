using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Business.Model;

namespace SaaSEqt.eShop.Services.Business.API.Extensions
{
    public static class SiteExtensions
    {
        public static void FillSiteUrl(this Site site, string picBaseUrl, bool azureStorageEnabled)
        {
            if (site.Branding == null) return;

            site.Branding.LogoUri = azureStorageEnabled
                ? picBaseUrl + site.Branding.LogoUri
                : picBaseUrl.Replace("[0]", site.Id.ToString());
        }
    }
}
