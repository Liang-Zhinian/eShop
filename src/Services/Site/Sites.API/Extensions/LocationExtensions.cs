﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Sites.API.Model.Security;

namespace SaaSEqt.eShop.Services.Sites.API.Extensions
{
    public static class LocationExtensions
    {
        public static void FillLocationUrl(this Location location, string picBaseUrl, bool azureStorageEnabled)
        {
            location.ImageUri = azureStorageEnabled
                ? picBaseUrl + location.Image
                : picBaseUrl.Replace("[0]", location.SiteId.ToString())
                            .Replace("[1]", location.Id.ToString());
        }
    }
}
