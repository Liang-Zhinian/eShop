using Eva.eShop.Services.Identity.API.Models;
using Eva.eShop.Services.Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Eva.eShop.Services.Identity.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationService _appService;

        public ApplicationsController(ApplicationService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApplicationViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ApplicationViewModel>> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var client = await _appService.GetAsync(id);

            if (client != null)
            {
                return client;
            }

            return NotFound();
        }

        [Route("implicit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateImplicitApplicationAsync([FromBody]CreateApplicationViewModel applicationViewModel)
        {
            var clientEntity = await _appService.CreateImplicitApplicationAsync(applicationViewModel);

            return CreatedAtAction(nameof(GetAsync), new { id = clientEntity.Id }, null);
        }

        [Route("oauth")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateOAuthApplicationAsync([FromBody]CreateApplicationViewModel applicationViewModel)
        {
            var clientEntity = await _appService.CreateOAuthApplicationAsync(applicationViewModel);

            return CreatedAtAction(nameof(GetAsync), new { id = clientEntity.Id }, null);
        }
    }
}
