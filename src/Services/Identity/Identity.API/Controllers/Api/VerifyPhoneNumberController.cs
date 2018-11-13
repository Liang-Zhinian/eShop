using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Infrastructure.Services;
using Identity.API.ViewModels;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SaaSEqt.eShop.Services.Identity.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.API.Controllers.Api
{
    [Route("api/v1/[controller]")]
    public class VerifyPhoneNumberController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ISmsService _smsService;
        private readonly DataProtectorTokenProvider<ApplicationUser> _dataProtectorTokenProvider;
        private readonly PhoneNumberTokenProvider<ApplicationUser> _phoneNumberTokenProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public VerifyPhoneNumberController(
            IConfiguration configuration,
            ISmsService smsService,
            DataProtectorTokenProvider<ApplicationUser> dataProtectorTokenProvider,
            PhoneNumberTokenProvider<ApplicationUser> phoneNumberTokenProvider,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
            _dataProtectorTokenProvider = dataProtectorTokenProvider ?? throw new ArgumentNullException(nameof(dataProtectorTokenProvider));
            _phoneNumberTokenProvider = phoneNumberTokenProvider ?? throw new ArgumentNullException(nameof(phoneNumberTokenProvider));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PhoneLoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await GetUser(model);
            var response = await SendSmsRequest(model, user);
            if (!response.Result) { return BadRequest("Sending sms failed"); }

            var resendToken = await _dataProtectorTokenProvider.GenerateAsync("resend_token", _userManager, user);
            var body = GetBody(response.VerifyToken, resendToken);

            return Accepted(body);
        }

        [HttpPut]
        public async Task<IActionResult> Put(string resendToken, [FromBody] PhoneLoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await GetUser(model);
            if (!await _dataProtectorTokenProvider.ValidateAsync("resend_token", resendToken, _userManager, user)){
                return BadRequest("Invalid resend token");
            }
                
            var response = await SendSmsRequest(model, user);
            if (!response.Result) { return BadRequest("Sending sms failed"); }

            var newResendToken = await _dataProtectorTokenProvider.GenerateAsync("resend_token", _userManager, user);
            var body = GetBody(response.VerifyToken, newResendToken);

            return Accepted(body);
        }

        private async Task<ApplicationUser> GetUser(PhoneLoginVm model)
        {
            var phoneNumber = _userManager.NormalizeKey(model.PhoneNumber);
            var user = await _userManager.Users.SingleOrDefaultAsync(y => y.PhoneNumber == phoneNumber)
                                         ?? new ApplicationUser
                                         {
                                             PhoneNumber = model.PhoneNumber,
                                             SecurityStamp = model.PhoneNumber.Sha256()
                                         };
            return user;
        }

        private async Task<(string VerifyToken, bool Result)> SendSmsRequest(PhoneLoginVm model, ApplicationUser user)
        {
            var verifyToken = await _phoneNumberTokenProvider.GenerateAsync("verify_number", _userManager, user);
            var result = await _smsService.SendAsync(model.PhoneNumber, $"{verifyToken}");
            return (verifyToken, result);
        }

        private Dictionary<string, string> GetBody(string verifyToken, string resendToken)
        {
            var body = new Dictionary<string, string> { { "resend_token", resendToken } };
            if (_configuration["ReturnVerifyTokenForTesting"] == bool.TrueString)
            {
                body.Add("verify_token", verifyToken);
            }

            return body;
        }
    }
}
