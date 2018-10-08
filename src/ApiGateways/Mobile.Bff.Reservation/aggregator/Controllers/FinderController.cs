using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sites.API.Controllers
{
    /*
FinderService.AddOrUpdateFinderUsers
FinderService.FinderCheckoutShoppingCart
FinderService.GetBusinessLocationsWithinRadius
FinderService.GetClassesWithinRadius
FinderService.GetFinderUser
FinderService.GetSessionTypesWithinRadius
FinderService.SendFinderUserNewPassword
    */
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class FinderController : Controller
    {
        private readonly IServiceCatalogService _serviceCatalogService;
        private readonly IBusinessLocationService _businessLocationService;

        public FinderController(IServiceCatalogService serviceCatalogService, IBusinessLocationService businessLocationService)
        {
            _serviceCatalogService = serviceCatalogService;
            _businessLocationService = businessLocationService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetBusinessLocationsWithinRadius(double latitude, double longitude, double radius, string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var locations = await _businessLocationService.GetBusinessLocationsWithinRadius(latitude, longitude, radius, searchText, pageSize, pageIndex);

            return Ok(locations);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSeriviceItemsWithinRadius(double latitude, double longitude, double radius, string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var serviceItems = await _serviceCatalogService.GetSeriviceItemsWithinRadius(latitude, longitude, radius, searchText, pageSize, pageIndex);


            return Ok(serviceItems);
        }

    }
}
