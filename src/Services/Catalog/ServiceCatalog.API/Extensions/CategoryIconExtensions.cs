using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Model
{
    public static class CategoryIconExtensions
    {
        public static void FillCategoryIconUrl(this CategoryIcon categoryIcon, string picBaseUrl, bool azureStorageEnabled)
        {
            categoryIcon.IconUri = azureStorageEnabled
                ? picBaseUrl + categoryIcon.IconFileName
                : picBaseUrl.Replace("[0]", categoryIcon.Id.ToString());
        }
    }
}
