namespace Eva.eShop.Services.Marketing.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    // GET: /<controller>/
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}