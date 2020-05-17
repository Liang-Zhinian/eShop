using System.ComponentModel.DataAnnotations;

namespace Eva.eShop.Services.Identity.API.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
