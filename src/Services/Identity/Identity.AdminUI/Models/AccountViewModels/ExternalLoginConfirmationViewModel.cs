using System.ComponentModel.DataAnnotations;

namespace SaaSEqt.eShop.Services.IdentityManagement.API.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
