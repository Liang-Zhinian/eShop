// Author: 	Liang Zhinian
// On:		2020/9/19
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.API.Models;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers
{
    [Route("api/v1/[controller]")]
    //[Authorize]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ConfigurationDbContext _context;

        public ApplicationsController(ConfigurationDbContext context)
        {
            _context = context;
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

            var client = await _context.Clients
                .Include(c=>c.RedirectUris)
                .Include(c=>c.ClientSecrets)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (client != null)
            {
                ApplicationViewModel application = new ApplicationViewModel
                {
                    Id = client.Id,
                    ClientId = client.ClientId,
                    ClientSecret = client.ClientSecrets?.FirstOrDefault()?.Value,
                    ApplicationName = client.ClientName,
                    ApplicationDescription = client.Description,
                    HomepageURL = client.ClientUri,
                    AuthorizationCallbackURL = client.RedirectUris[0].RedirectUri
                };
                return application;
            }

            return NotFound();
        }

        [Route("implicit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateImplicitApplicationAsync([FromBody]CreateApplicationViewModel applicationViewModel)
        {
            Client client = new Client
            {
                ClientId = Guid.NewGuid().ToString().Replace("-", ""),
                ClientName = applicationViewModel.ApplicationName,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                ClientUri = applicationViewModel.HomepageURL,
                RedirectUris = { $"{applicationViewModel.AuthorizationCallbackURL}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{applicationViewModel.HomepageURL}/swagger/" },

                AllowedScopes =
                    {
                        "orders"
                    }
            };

            IdentityServer4.EntityFramework.Entities.Client clientEntity = client.ToEntity();
            clientEntity.Description = applicationViewModel.ApplicationDescription;

            _context.Clients.Add(clientEntity);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsync), new { id = clientEntity.Id }, null);
        }

        [Route("oauth")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateOAuthApplicationAsync([FromBody]CreateApplicationViewModel applicationViewModel)
        {
            Client client = new Client
            {
                ClientId = Guid.NewGuid().ToString().Replace("-", ""),
                ClientName = applicationViewModel.ApplicationName,
                ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                ClientUri = $"{applicationViewModel.HomepageURL}", //public uri of the client
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials, // or GrantTypes.Code
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                RedirectUris = new List<string>
                    {
                        $"{applicationViewModel.AuthorizationCallbackURL}/signin-oidc"
                    },
                PostLogoutRedirectUris = new List<string>
                    {
                        $"{applicationViewModel.HomepageURL}/signout-callback-oidc"
                    },
                AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "orders",
                        "basket",
                        "locations",
                        "marketing",
                        "webshoppingagg",
                        "orders.signalrhub",
                        "webhooks"
                    },
                AccessTokenLifetime = 60 * 60 * 2, // 2 hours
                IdentityTokenLifetime = 60 * 60 * 2 // 2 hours
            };

            IdentityServer4.EntityFramework.Entities.Client clientEntity = client.ToEntity();
            clientEntity.Description = applicationViewModel.ApplicationDescription;

            _context.Clients.Add(clientEntity);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsync), new { id = clientEntity.Id }, null);
        }
    }
}
