using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserManagement.Models;
using SaaSEqt.eShop.Services.Identity.API.Models;

namespace UserManagement.Utilities
{
    public class EmailUserValidator:IUserValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            if (user.Email.ToLower().EndsWith("@example.com"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailDomainError",
                    Description = "example.com email addresses are NOT allowed."
                }));
            }
            else
            {
                return Task.FromResult(IdentityResult.Success);
            }
        }
    }
}
