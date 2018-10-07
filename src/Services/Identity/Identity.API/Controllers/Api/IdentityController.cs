using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.Identity.API.Models;

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

        // GET: api/values
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/values/1
        [HttpGet("{id:int}")]
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("users/with-user-name/{username:minlength(1)}")]
        public async Task<IActionResult> FindByUsername(string username)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            return Ok(user);
        }
    }
}
