﻿namespace Eva.eShop.Web.Shopping.HttpAggregator.Controllers;

[Route("")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return new RedirectResult("~/swagger");
    }
}
