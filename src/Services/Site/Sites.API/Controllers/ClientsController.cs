using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Sites.API;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
using SaaSEqt.eShop.Services.Sites.API.Model;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;
using Sites.API.IntegrationEvents;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sites.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ClientsController : Controller
    {
        private readonly SitesContext _sitesContext;
        private readonly IHostingEnvironment _env;
        private readonly SitesSettings _settings;
        private readonly ISitesIntegrationEventService _sitesIntegrationEventService;

        public ClientsController(SitesContext context, ISitesIntegrationEventService eShopIntegrationEventService,
                                   IHostingEnvironment env, IOptionsSnapshot<SitesSettings> settings)
        {
            _sitesContext = context ?? throw new ArgumentNullException(nameof(context));
            _sitesIntegrationEventService = eShopIntegrationEventService;
            _env = env;
            _settings = settings.Value;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/v1/clients/{guid}
        [HttpGet("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Site), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var client = await _sitesContext.Clients.SingleOrDefaultAsync(y => y.Id.Equals(id));

            if (client != null)
            {
                return Ok(client);
            }

            return NotFound();
        }

        // GET api/v1/sites/{siteid:guid}clients/demouser[?pagesize=10&pageindex=0]
        [HttpGet]
        [Route("sites/{siteId:Guid}/clients/searchtext")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Client>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid siteId, string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            if (siteId == Guid.Empty)
            {
                return BadRequest();
            }

            var root = (IQueryable<Client>)_sitesContext.Clients.Where(y => y.SiteId.Equals(siteId));
                                                        
            if(!string.IsNullOrEmpty(searchText)){
                root = root.Where(y => y.MobilePhone.Contains(searchText)
                                  || y.HomePhone.Contains(searchText)
                                  || y.WorkPhone.Contains(searchText)
                                  || y.FullName.Contains(searchText)
                                  || y.EmailAddress.Contains(searchText));
            }

            var totalItems = await root.LongCountAsync();

            var itemsOnPage = root
                .OrderBy(c => c.FirstName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedItemsViewModel<Client>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        // POST api/v1/clients
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody]Client client)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)BadRequest();
            }

            _sitesContext.Clients.Add(client);
            await _sitesContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = client.Id }, null);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
