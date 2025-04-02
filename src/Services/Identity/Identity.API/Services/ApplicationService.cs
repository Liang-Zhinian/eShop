using Eva.eShop.Services.Identity.API.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;

namespace Eva.eShop.Services.Identity.API.Services
{
    public class ApplicationService
    {
        private readonly ConfigurationDbContext _context;

        public ApplicationService(ConfigurationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationViewModel> GetAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var client = await _context.Clients
                .Include(c => c.RedirectUris)
                .Include(c => c.ClientSecrets)
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

            return null;
        }

        public async Task<IdentityServer4.EntityFramework.Entities.Client> CreateImplicitApplicationAsync(CreateApplicationViewModel applicationViewModel)
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

                AllowedScopes = applicationViewModel.AllowedScopes
            };

            IdentityServer4.EntityFramework.Entities.Client clientEntity = client.ToEntity();
            clientEntity.Description = applicationViewModel.ApplicationDescription;

            _context.Clients.Add(clientEntity);

            await _context.SaveChangesAsync();

            return clientEntity;
        }

        public async Task<IdentityServer4.EntityFramework.Entities.Client> CreateOAuthApplicationAsync(CreateApplicationViewModel applicationViewModel)
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
                AllowedScopes = applicationViewModel.AllowedScopes/*new List<string>
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
                    }*/,
                AccessTokenLifetime = 60 * 60 * 2, // 2 hours
                IdentityTokenLifetime = 60 * 60 * 2 // 2 hours
            };

            IdentityServer4.EntityFramework.Entities.Client clientEntity = client.ToEntity();
            clientEntity.Description = applicationViewModel.ApplicationDescription;

            _context.Clients.Add(clientEntity);

            await _context.SaveChangesAsync();

            return clientEntity;
        }
    }
}
