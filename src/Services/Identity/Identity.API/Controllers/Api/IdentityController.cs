using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Eva.eShop.Services.Identity.API.Models;
using Eva.eShop.Services.Identity.API.Models.AccountViewModels;
using System.IO;
using System.Net;
using Identity.ViewModels;
using Microsoft.EntityFrameworkCore;
using Eva.eShop.Services.Identity.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Eva.eShop.Services.Identity.API;
using Microsoft.Extensions.Options;
using Eva.eShop.Services.Identity.API.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly AppSettings _settings;
        private readonly ApplicationDbContext _context;

        public IdentityController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
                                  IHostingEnvironment env,
                                  IOptionsSnapshot<AppSettings> settings)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
            _settings = settings.Value;
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.User == null) model.User = ApplicationUser.Empty();

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
                    SecurityNumber = model.User.SecurityNumber,
                    //Gender = model.User.Gender
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
        [AllowAnonymous]
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
            if (!resetPasswordResult.Succeeded)
            {
                AddErrors(resetPasswordResult);
                return BadRequest();
            }

            return Ok("Password updated");
        }

        [Route("users")]
        [HttpPut]
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
            //user.Gender = userToUpdate.Gender;

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                AddErrors(updateUserResult);
                return BadRequest();
            }

            return Ok("User updated");
        }

        [Route("users")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody]CreateExternalAccountVm userToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = ApplicationUser.Empty();
            //user.GenderId = userToCreate.Gender;
            user.UserName = await _userManager.GenerateConcurrencyStampAsync(user);

            if (userToCreate != null){
                //user.ExternalAccounts = new ExternalAccounts();
                //user.ExternalAccounts.WechatOpenId = userToCreate.WechatOpenId;
                //user.ExternalAccounts.AlipayUserId = userToCreate.AlipayUserId;
            }

            var updateUserResult = await _userManager.CreateAsync(user);

            if (!updateUserResult.Succeeded)
            {
                AddErrors(updateUserResult);
                return BadRequest();
            }

            return Ok("User created");
        }

        [Route("users/avatar")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserAvatar([FromForm]UploadUserAvatarVm userAvatarToUpload)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userAvatarToUpload.UserId.ToString());
            if (user == null) throw new InvalidOperationException("User does not exist");

            using (var stream = new MemoryStream())
            {
                await userAvatarToUpload.Image.CopyToAsync(stream);
                //user.AvatarImage = stream.ToArray();
            }

            //user.AvatarImageFileName = userAvatarToUpload.Image.FileName;

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                AddErrors(updateUserResult);
                return BadRequest();
            }

            return Ok("User updated");
        }

        //[HttpGet]
        //[Route("users/{id:guid}/pic")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetUserAvatar(Guid id)
        //{
        //    ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());

        //    if (user != null && user.AvatarImage != null)
        //    {
        //        var buffer = user.AvatarImage;

        //        string mime = "image/jpeg";
        //        if (!string.IsNullOrEmpty(user.AvatarImageFileName))
        //        {
        //            string imageFileExtension = Path.GetExtension(user.AvatarImageFileName);
        //            mime = GetImageMimeTypeFromImageFileExtension(imageFileExtension);
        //        }

        //        return File(buffer, mime);
        //    }

        //    return Ok(null);
        //}

        [HttpGet]
        [Route("users/with-user-name/{username:minlength(1)}")]
        public async Task<IActionResult> FindByUsername(string username)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            var baseUri = _settings.PicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;
            user.FillUserAvatarUrl(baseUri, azureStorageEnabled);

            return Ok(user);
        }

        //[HttpGet]
        //[Route("users/with-external-accounts")]
        //public async Task<IActionResult> FindByExternalAccounts([FromQuery]string facebookEmail, 
        //                                                        [FromQuery]string twitterUsername, 
        //                                                        [FromQuery]string wechatOpenId, 
        //                                                        [FromQuery]string alipayOpenId, 
        //                                                        [FromQuery]string pay2OpenId)
        //{
        //    var root = (IQueryable<ExternalAccounts>)_context.ExternalAccounts;

        //    if (!string.IsNullOrEmpty(facebookEmail)){
        //        root = root.Where(y => y.FacebookEmail.ToLower() == facebookEmail.ToLower());
        //    }
        //    if (!string.IsNullOrEmpty(twitterUsername))
        //    {
        //        root = root.Where(y => y.TwitterUsername.ToLower() == twitterUsername.ToLower());
        //    }
        //    if (!string.IsNullOrEmpty(wechatOpenId))
        //    {
        //        root = root.Where(y => y.WechatOpenId.ToLower() == wechatOpenId.ToLower());
        //    }
        //    if (!string.IsNullOrEmpty(alipayOpenId))
        //    {
        //        root = root.Where(y => y.AlipayUserId.ToLower() == alipayOpenId.ToLower());
        //    }
        //    if (!string.IsNullOrEmpty(pay2OpenId))
        //    {
        //        root = root.Where(y => y.Pay2OpenId.ToLower() == pay2OpenId.ToLower());
        //    }

        //    ExternalAccounts externalAccount = await root.SingleOrDefaultAsync(y=>true);

        //    ApplicationUser user = await _userManager.FindByIdAsync(externalAccount.UserId);

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    var baseUri = _settings.PicBaseUrl;
        //    var azureStorageEnabled = _settings.AzureStorageEnabled;
        //    user.FillUserAvatarUrl(baseUri, azureStorageEnabled);

        //    return Ok(user);
        //}

        // GET api/v1/users?searchtext=demouser[&pagesize=10&pageindex=0]
        [HttpGet]
        [Route("users")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ApplicationUserVm>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchUsers([FromQuery]string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ApplicationUser>)_userManager.Users;

            if (root == null || root.Count() == 0) return NotFound();

            if (!string.IsNullOrEmpty(searchText))
            {
                root = root.Where(user => Contains(user.PhoneNumber, searchText)
                                  //|| Contains(user.FullName, searchText)
                                  || Contains(user.Email, searchText));
            }

            var totalItems = await root.LongCountAsync();

            var itemsOnPage = root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            itemsOnPage = ChangeUserUriPlaceholder(itemsOnPage);

            List<ApplicationUserVm> applicationUsers = new List<ApplicationUserVm>();
            itemsOnPage.ForEach((item) =>
            {
                applicationUsers.Add(new ApplicationUserVm
                {
                    Id = Guid.Parse(item.Id),
                    Street = item.Street,
                    City = item.City,
                    State = item.State,
                    Country = item.Country,
                    ZipCode = item.ZipCode,
                    Name = item.Name,
                    LastName = item.LastName,
                    //AvatarImage = item.AvatarImage,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    //AvatarImageUri = item.AvatarImageUri,
                    //AvatarImageFileName = item.AvatarImageFileName,
                });
            });



            var model = new PaginatedItemsViewModel<ApplicationUserVm>(
                pageIndex, pageSize, totalItems, applicationUsers);

            return Ok(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private bool Contains(string left, string right)
        {
            return !string.IsNullOrEmpty(left) && left.Contains(right);
        }


        private List<ApplicationUser> ChangeUserUriPlaceholder(List<ApplicationUser> users)
        {
            var baseUri = _settings.PicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            foreach (var user in users)
            {
                user.FillUserAvatarUrl(baseUri, azureStorageEnabled: azureStorageEnabled);

            }

            return users;
        }

        private string GetImageMimeTypeFromImageFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".png":
                    mimetype = "image/png";
                    break;
                case ".gif":
                    mimetype = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimetype = "image/jpeg";
                    break;
                case ".bmp":
                    mimetype = "image/bmp";
                    break;
                case ".tiff":
                    mimetype = "image/tiff";
                    break;
                case ".wmf":
                    mimetype = "image/wmf";
                    break;
                case ".jp2":
                    mimetype = "image/jp2";
                    break;
                case ".svg":
                    mimetype = "image/svg+xml";
                    break;
                default:
                    mimetype = "application/octet-stream";
                    break;
            }

            return mimetype;
        }
    }
}
