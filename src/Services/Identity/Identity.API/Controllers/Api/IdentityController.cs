using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.Identity.API.Models;
using SaaSEqt.eShop.Services.Identity.API.Models.AccountViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IdentityController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    CardHolderName = model.User.CardHolderName,
                    CardNumber = model.User.CardNumber,
                    CardType = model.User.CardType,
                    City = model.User.City,
                    Country = model.User.Country,
                    Expiration = model.User.Expiration,
                    LastName = model.User.LastName,
                    Name = model.User.Name,
                    Street = model.User.Street,
                    State = model.User.State,
                    ZipCode = model.User.ZipCode,
                    PhoneNumber = model.User.PhoneNumber,
                    SecurityNumber = model.User.SecurityNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Errors.Count() > 0)
                {
                    AddErrors(result);
                    // If we got this far, something failed, redisplay form
                    return BadRequest(ModelState);
                }
            }
            return Ok(model);
        }

        [Route("forgot-password")]
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromQuery]ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
                return Content("Check your email for a password reset link");

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(passwordResetToken);    
        }

        [Route("reset-password")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.Email);
            if (user == null) throw new InvalidOperationException();

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match");
                return BadRequest();
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!resetPasswordResult.Succeeded){
                AddErrors(resetPasswordResult);
                return BadRequest();
            }

            return Ok("Password updated");
        }

        [Route("users")]
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser([FromBody]ApplicationUser userToUpdate)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userToUpdate.Id);
            if (user == null) throw new InvalidOperationException("User does not exist");

            user.UserName = userToUpdate.UserName;
            user.Email = userToUpdate.Email;
            user.CardHolderName = userToUpdate.CardHolderName;
            user.CardNumber = userToUpdate.CardNumber;
            user.CardType = userToUpdate.CardType;
            user.City = userToUpdate.City;
            user.Country = userToUpdate.Country;
            user.Expiration = userToUpdate.Expiration;
            user.LastName = userToUpdate.LastName;
            user.Name = userToUpdate.Name;
            user.Street = userToUpdate.Street;
            user.State = userToUpdate.State;
            user.ZipCode = userToUpdate.ZipCode;
            user.PhoneNumber = userToUpdate.PhoneNumber;
            user.SecurityNumber = userToUpdate.SecurityNumber;

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                AddErrors(updateUserResult);
                return BadRequest();
            }

            return Ok("User updated");
        }


        [HttpGet]
        [Route("users/with-user-name/{username:minlength(1)}")]
        public async Task<IActionResult> FindByUsername(string username)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            return Ok(user);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
