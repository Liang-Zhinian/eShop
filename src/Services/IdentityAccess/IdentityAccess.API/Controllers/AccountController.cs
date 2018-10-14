using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.Services.IdentityAccess.API.ViewModel;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using SaaSEqt.eShop.Services.IdentityAccess.API.Application.IntegrationEvents.Events.Staffs;

namespace SaaSEqt.eShop.Services.IdentityAccess.API.Controllers
{
    //[Route("api/v1/[controller]")]
    //[Authorize]
    public partial class IdentityAccessController // : Controller
    {
        //private readonly IdentityApplicationService _identityApplicationService;

        //public IdentityAccessController(IdentityApplicationService identityApplicationService)
        //{
        //    _identityApplicationService = identityApplicationService;
        //}

        // GET api/v1/account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/v1/[controller]/tenants/guid/staffs?userName={userName:minlength(1)}
        [HttpGet]
        [Route("tenants/{tenantId:Guid}/staffs")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StaffViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Staffs(Guid tenantId, [FromQuery]string userName)
        {
            if (tenantId == Guid.Empty || string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var user = _identityApplicationService.GetUser(tenantId.ToString(), userName);

            if (user == null) return NotFound();

            StaffViewModel staff = new StaffViewModel
            {
                FirstName = user.Person.Name.FirstName,
                LastName = user.Person.Name.LastName,
                EmailAddress = user.Person.EmailAddress.Address,
                PrimaryTelephone = user.Person.ContactInformation.PrimaryTelephone.Number,
                SecondaryTelephone = user.Person.ContactInformation.SecondaryTelephone.Number,
                AddressStreetAddress = user.Person.ContactInformation.PostalAddress.StreetAddress,
                AddressCity = user.Person.ContactInformation.PostalAddress.City,
                AddressStateProvince = user.Person.ContactInformation.PostalAddress.StateProvince,
                AddressPostalCode = user.Person.ContactInformation.PostalAddress.PostalCode,
                AddressCountryCode = user.Person.ContactInformation.PostalAddress.CountryCode,
            };

            return Ok(staff);
        }

        // POST api/v1/[controller]/tenants/guid/staffs
        [HttpPost]
        [Route("tenants/{tenantId:Guid}/staffs")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Staffs(Guid tenantId, [FromBody]RegisterUserCommand registerUserCommand)
        {
            var newUser = _identityApplicationService.RegisterUser(registerUserCommand);
            await _identityAccessContext.SaveChangesAsync();

            StaffCreatedEvent staffCreatedEvent = new StaffCreatedEvent(newUser.Id, newUser.TenantId,
                                                                        newUser.Username, newUser.Password,
                                                                        newUser.Person.Name.FirstName, newUser.Person.Name.LastName, newUser.IsEnabled,
                                                                        newUser.Enablement.StartDate, newUser.Enablement.EndDate,
                                                                        newUser.Person.EmailAddress.Address, newUser.Person.ContactInformation.PrimaryTelephone.Number, 
                                                                        newUser.Person.ContactInformation.SecondaryTelephone.Number,
                                                                        newUser.Person.ContactInformation.PostalAddress.StreetAddress, 
                                                                        newUser.Person.ContactInformation.PostalAddress.City, 
                                                                        newUser.Person.ContactInformation.PostalAddress.StateProvince,
                                                                        newUser.Person.ContactInformation.PostalAddress.PostalCode, 
                                                                        newUser.Person.ContactInformation.PostalAddress.CountryCode
            );

            await _identityAccessIntegrationEventService.SaveEventAndContextChangesAsync(staffCreatedEvent);

            // Publish through the Event Bus and mark the saved event as published
            await _identityAccessIntegrationEventService.PublishThroughEventBusAsync(staffCreatedEvent);

            return CreatedAtAction(nameof(Staffs), new { tenantId = registerUserCommand.TenantId, userName = registerUserCommand.Username }, null);
        }

        // PUT api/v1/[controller]/tenants/guid/staffs/guid
        [HttpPut("tenants/{tenantId:Guid}/staffs/{staffId:Guid}")]
        public void Put(Guid tenantId, Guid staffId, [FromBody]string value)
        {
        }

        // DELETE api/v1/[controller]/tenants/guid/staffs/guid
        [HttpDelete("tenants/{tenantId:Guid}/staffs/{staffId:Guid}")]
        public void Delete(Guid tenantId, Guid staffId)
        {
        }
    }
}
