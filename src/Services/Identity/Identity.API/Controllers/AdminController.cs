using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.Identity.API.Models;
using UserManagement.Models;
using UserManagement.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        private Task<ApplicationUser> CurrentUser =>
            userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public AdminController(UserManager<ApplicationUser> userMgr)
        {
            userManager = userMgr;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(GetAuthInfo(nameof(Index), nameof(AdminController)));
        }

        private Dictionary<string, object> GetAuthInfo(string actionName, string controllerName)
        {
            return new Dictionary<string, object>
            {
                // ["Controller"] = controllerName,
                // ["Action"] = actionName,
                ["User"] = HttpContext.User.Identity.Name,
                ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
                ["Authentication Type"] = HttpContext.User.Identity.AuthenticationType,
                ["In SuperAdmins Role"] = HttpContext.User.IsInRole("SuperAdmins"),
                ["In Staff Role"] = HttpContext.User.IsInRole("Staff"),
                ["In Customers Role"] = HttpContext.User.IsInRole("Customers"),
                //["Continent"] = CurrentUser.Result.Continent,
                //["ExperienceLevels"] = CurrentUser.Result.ExperienceLevel
            };
        }

        public async Task<IActionResult> SetUserCustomProps()
        {
            return View(await CurrentUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetUserCustomProps(SetCustomPropsVm customProps)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await CurrentUser;

                //user.Continent = customProps.Continent;
                //user.ExperienceLevel = customProps.ExperienceLevel;
                await userManager.UpdateAsync(user);

                return RedirectToAction("Index");
            }

            return View(await CurrentUser);
        }

        [Authorize]
        public IActionResult ForNzlCustomers()
        {
            return View();
        }

        [Authorize]
        public IActionResult Block()
        {
            return View();
        }
    }
}
