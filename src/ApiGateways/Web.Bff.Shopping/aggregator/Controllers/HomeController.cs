﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Web.Shopping.HttpAggregator.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
