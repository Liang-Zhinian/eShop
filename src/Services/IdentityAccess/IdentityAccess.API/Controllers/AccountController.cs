using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.IdentityAccess.API.ViewModel;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Application.Commands;

namespace SaaSEqt.IdentityAccess.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly IdentityApplicationService _identityApplicationService;

        public AccountController(IdentityApplicationService identityApplicationService)
        {
            _identityApplicationService = identityApplicationService;
        }

        // GET api/v1/account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/v1/account?tenantId={tenantId:guid}&userName={userName:minlength(1)}
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StaffViewModel), (int)HttpStatusCode.OK)]
        public IActionResult GetUser([FromQuery]string tenantId, [FromQuery]string userName)
        {
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(userName)){
                return BadRequest();
            }

            var user = _identityApplicationService.GetUser(tenantId, userName);

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

        // POST api/v1/account
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public IActionResult Post([FromBody]RegisterUserCommand registerUserCommand)
        {
            var newUser = _identityApplicationService.RegisterUser(registerUserCommand);
            return CreatedAtAction(nameof(Get), new { tenantId = registerUserCommand.TenantId, userName = registerUserCommand.Username }, null);
        }

        // PUT api/v1/account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/v1/account/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
