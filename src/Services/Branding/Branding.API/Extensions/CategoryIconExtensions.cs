using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Branding.API.Model
{
    public static class CategoryIconExtensions
    {
        public static void FillCategoryIconUrl(this CategoryIcon categoryIcon, string picBaseUrl, bool azureStorageEnabled)
        {
            categoryIcon.IconUri = azureStorageEnabled
                ? picBaseUrl + categoryIcon.IconFileName
                : picBaseUrl.Replace("[0]", categoryIcon.Type)
                .Replace("[1]", categoryIcon.ServiceCategoryName)
                .Replace("[2]", categoryIcon.Language);


        }
    }
}
