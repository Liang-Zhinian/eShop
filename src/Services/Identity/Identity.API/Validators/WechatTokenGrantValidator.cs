﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SaaSEqt.eShop.Services.Identity.API.Models;

namespace Identity.API.Validators
{
    public class WechatTokenGrantValidator : IExtensionGrantValidator
    {
        //repository to get user from db
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEventService _events;
        private readonly ILogger<PhoneNumberTokenGrantValidator> _logger;

        public WechatTokenGrantValidator(UserManager<ApplicationUser> userManager,
                                              SignInManager<ApplicationUser> signInManager,
                                              IEventService events,
                                              ILogger<PhoneNumberTokenGrantValidator> logger)
        {
            _userManager = userManager; //DI
            _signInManager = signInManager;
            _events = events;
            _logger = logger;
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            try
            {
                var createUser = false;
                var raw = context.Request.Raw;
                var credential = raw.Get(OidcConstants.TokenRequest.GrantType);
                if (credential == null || credential != "wechat_token")
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid verify_wechat_token credential");
                    return;
                }

                var openid = raw.Get("openid");

                //get your user model from db (by username - in my case its email)
                var user = await _userManager.Users
                                             .Include(y => y.ExternalAccounts)
                                             .SingleOrDefaultAsync(y => y.ExternalAccounts.WechatOpenId == openid);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = openid,
                        ExternalAccounts = new ExternalAccounts
                        {
                            WechatOpenId = openid
                        }
                    };

                    createUser = true;
                }

                //var result = await _phoneNumberTokenProvider.ValidateAsync("verify_number", verificationToken, _userManager, user);
                //if (!result)
                //{
                //    _logger.LogInformation("Authentication failed for token: {token}, reason: invalid token", verificationToken);
                //    await _events.RaiseAsync(new UserLoginFailureEvent(verificationToken, "invalid token or verification id", false));
                //    return;
                //}

                if (createUser)
                {
                    //user.PhoneNumberConfirmed = true;
                    var resultCreation = await _userManager.CreateAsync(user);
                    if (resultCreation != IdentityResult.Success)
                    {
                        _logger.LogInformation("User creation failed: {openid}, reason: invalid user", openid);
                        await _events.RaiseAsync(new UserLoginFailureEvent(openid, resultCreation.Errors.Select(y => y.Description).Aggregate((a, b) => a + ", " + b), false));
                        return;
                    }
                }

                _logger.LogInformation("Credentials validated for openid: {openid}", openid);
                await _events.RaiseAsync(new UserLoginSuccessEvent(openid, user.Id, openid, false));
                await _signInManager.SignInAsync(user, true);
                context.Result = new GrantValidationResult(
                                            user.Id,
                                            "Wechat"
                                        );
                return;

            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid openid");
            }
        }

        //build claims array from user data
        private IEnumerable<Claim> GetClaimsFromUser(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            if (!string.IsNullOrWhiteSpace(user.Name))
                claims.Add(new Claim("name", user.Name));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                claims.Add(new Claim("last_name", user.LastName));

            if (!string.IsNullOrWhiteSpace(user.CardNumber))
                claims.Add(new Claim("card_number", user.CardNumber));

            if (!string.IsNullOrWhiteSpace(user.CardHolderName))
                claims.Add(new Claim("card_holder", user.CardHolderName));

            if (!string.IsNullOrWhiteSpace(user.SecurityNumber))
                claims.Add(new Claim("card_security_number", user.SecurityNumber));

            if (!string.IsNullOrWhiteSpace(user.Expiration))
                claims.Add(new Claim("card_expiration", user.Expiration));

            if (!string.IsNullOrWhiteSpace(user.City))
                claims.Add(new Claim("address_city", user.City));

            if (!string.IsNullOrWhiteSpace(user.Country))
                claims.Add(new Claim("address_country", user.Country));

            if (!string.IsNullOrWhiteSpace(user.State))
                claims.Add(new Claim("address_state", user.State));

            if (!string.IsNullOrWhiteSpace(user.Street))
                claims.Add(new Claim("address_street", user.Street));

            if (!string.IsNullOrWhiteSpace(user.ZipCode))
                claims.Add(new Claim("address_zip_code", user.ZipCode));

            if (_userManager.SupportsUserEmail)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            if (_userManager.SupportsUserRole)
            {
                claims.AddRange(_userManager.GetRolesAsync(user).Result.Select(y => new Claim(JwtClaimTypes.Role, y)));
            }

            return claims;
        }

        public string GrantType => "wechat_token";
    }
}
