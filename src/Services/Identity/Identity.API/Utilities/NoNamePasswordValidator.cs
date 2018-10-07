using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SaaSEqt.eShop.Services.Identity.API.Models;

namespace Identity.Utilities
{
    public class NoNamePasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "PasswordContainsUserName",
                    Description = "Password cannot contain your user name."
                }));
            }
            else
            {
                return Task.FromResult(IdentityResult.Success);
            }
            
        }

    }
}
