using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SaaSEqt.eShop.Services.Business.API.ViewModel;
using SaaSEqt.eShop.Services.Business.API.Infrastructure.Services;
using SaaSEqt.eShop.Services.Business.API.Application.Events;
using System.Net;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    //[Authorize]
    [Route("api/tenants")]
    public class TenantController: Controller
    {
        private readonly ITenantService _tenantService;
        private readonly IIdentityAccessIntegrationEventService _identityAccessIntegrationEventService;

        public TenantController(IIdentityAccessIntegrationEventService identityAccessIntegrationEventService,
            ITenantService tenantService)
        {
            _identityAccessIntegrationEventService = identityAccessIntegrationEventService;
            _tenantService = tenantService;
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            //var list = _bizService.GetAll()
            //                        .ToList();
            //return Json(list);
            throw new NotImplementedException();
        }

        [HttpGet]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("test")]
        public ActionResult Test()
        {
            return Ok("Success!");
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("register")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]
                                   TenantViewModel tenant,
                                   StaffViewModel administrator
                                  )
        {
            //throw new NotImplementedException();
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return Ok();
            }

            var newTenant = _tenantService.ProvisionTenant(tenant, administrator);


            TenantCreatedEvent tenantCreatedEvent = new TenantCreatedEvent(
                newTenant.Id,
                newTenant.Name,
                newTenant.Description
            );

            await _identityAccessIntegrationEventService.SaveEventAndContextChangesAsync(tenantCreatedEvent);

            // Publish through the Event Bus and mark the saved event as published
            await _identityAccessIntegrationEventService.PublishThroughEventBusAsync(tenantCreatedEvent);

            return Ok();
        }
    }
}
