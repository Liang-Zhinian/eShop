using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SaaSEqt.eShop.Services.IdentityManagement.API.Models.ManageViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}
