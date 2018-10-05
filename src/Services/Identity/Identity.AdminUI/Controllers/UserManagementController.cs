using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.IdentityManagement.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserManagementController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagementController(UserManager<ApplicationUser> userManager)
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

        [HttpGet]
        [Route("users/with-user-name/{username:minlength(1)}")]
        public async Task<IActionResult> FindByUsername(string username)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(username);

            return Ok(user);
        }
    }
}
