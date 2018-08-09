using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Business.Model;

namespace SaaSEqt.eShop.Services.Business.API.Extensions
{
	public static class LocationImageExtensions
    {
        public static void FillLocationImageUrl(this LocationImage locationImage, string picBaseUrl, bool azureStorageEnabled)
        {
            locationImage.ImageUri = azureStorageEnabled
                ? picBaseUrl + locationImage.Image
                : picBaseUrl.Replace("[0]", locationImage.SiteId.ToString())
                .Replace("[1]", locationImage.LocationId.ToString())
                .Replace("[2]", locationImage.Id.ToString());
        }
    }
}
