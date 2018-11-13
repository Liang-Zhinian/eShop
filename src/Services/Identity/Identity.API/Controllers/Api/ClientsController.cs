using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.API.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ClientsController : Controller
    {
        ConfigurationDbContext _context;
        public ClientsController(ConfigurationDbContext context)
        {
            _context = context;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Client client)
        {
            if (await _context.Clients.AnyAsync(y => y.ClientId == client.ClientId))
            {
                return BadRequest("Duplicate client was found");
            }

            await _context.Clients.AddAsync(client.ToEntity());
            var result = await _context.SaveChangesAsync();

            return Ok(client);
        }
    }
}
