using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.ViewModels
{
    public class ResetPasswordVm
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
