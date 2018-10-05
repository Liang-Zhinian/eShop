using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SaaSEqt.eShop.Services.IdentityAccess.API.ViewModel;
using SaaSEqt.eShop.Services.IdentityAccess.API.Infrastructure.Services;
using System.Net;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.IdentityAccess.API.Application.Events;
using SaaSEqt.eShop.Services.IdentityAccess.API.Application.IntegrationEvents;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Infrastructure.Context;

namespace SaaSEqt.eShop.Services.IdentityAccess.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public partial class IdentityAccessController: Controller
    {
        private readonly ITenantService _tenantService;
        private readonly IIdentityAccessIntegrationEventService _identityAccessIntegrationEventService;
        private readonly IdentityApplicationService _identityApplicationService;
        private readonly IdentityAccessDbContext _identityAccessContext;

        public IdentityAccessController(
            IdentityAccessDbContext identityAccessContext,
            IIdentityAccessIntegrationEventService identityAccessIntegrationEventService,
                                        ITenantService tenantService,
                                        IdentityApplicationService identityApplicationService)
        {
            _identityAccessContext = identityAccessContext;
            _identityAccessIntegrationEventService = identityAccessIntegrationEventService;
            _tenantService = tenantService;
            _identityApplicationService = identityApplicationService;
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("tenants")]
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

        [HttpPost]
        //[Authorize(Policy = "CanWriteTenantData")]
        [Route("tenants/{tenantId:Guid}/registration-invitations")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> OfferRegistrationInvitation(Guid tenantId, string description)
        {
            //throw new NotImplementedException();
            if (!ModelState.IsValid)
            {
                //NotifyModelStateErrors();
                return BadRequest();
            }

            var newInvitation = _tenantService.OfferRegistrationInvitation(tenantId, description);

            await _identityAccessContext.SaveChangesAsync();

            return Ok(newInvitation);
        }
    }
}
