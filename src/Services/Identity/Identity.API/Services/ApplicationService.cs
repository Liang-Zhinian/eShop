using Eva.eShop.Services.Identity.API.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Polly;

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
                .Include(c => c.AllowedScopes)
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
                    AuthorizationCallbackURL = client.RedirectUris[0].RedirectUri,
                    AllowedScopes = client.AllowedScopes.Select(s => s.Scope).ToList(),

                    //ApiResource = new ApiResourceViewModel
                };

                if (client.AllowedScopes != null && client.AllowedScopes.Count > 0)
                {
                    var apiResources = await _context.ApiResources
                        .Include(a => a.Scopes)
                        .Where(a => client.AllowedScopes.Select(s => s.Scope).Intersect(a.Scopes.Select(s => s.Name)).Any())
                        .ToListAsync();
                    application.ApiResource = apiResources.Select(a => new ApiResourceViewModel()
                    {
                        Name = a.Name,
                        DisplayName = a.DisplayName,
                        Description = a.Description,
                        UserClaims = a.UserClaims.Select(c => c.Type).ToList()
                    })
                        .First();
                }

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
                RedirectUris = { applicationViewModel.AuthorizationCallbackURL },
                PostLogoutRedirectUris = { applicationViewModel.HomepageURL },

                AllowedScopes = applicationViewModel.AllowedScopes
            };

            IdentityServer4.EntityFramework.Entities.Client clientEntity = client.ToEntity();
            clientEntity.Description = applicationViewModel.ApplicationDescription;

            _context.Clients.Add(clientEntity);

            ApiResource apiResource = new ApiResource(applicationViewModel.ApiResource.Name,
                applicationViewModel.ApiResource.DisplayName,
                applicationViewModel.ApiResource.UserClaims
                )
            {
                Description = applicationViewModel.ApiResource.Description,
                Enabled = true,
            };
            IdentityServer4.EntityFramework.Entities.ApiResource apiResourceEntity = apiResource.ToEntity();
            _context.ApiResources.Add(apiResourceEntity);

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

            ApiResource apiResource = new ApiResource(applicationViewModel.ApiResource.Name,
                applicationViewModel.ApiResource.DisplayName,
                applicationViewModel.ApiResource.UserClaims
                )
            {
                Description = applicationViewModel.ApiResource.Description,
                Enabled = true,
            };
            IdentityServer4.EntityFramework.Entities.ApiResource apiResourceEntity = apiResource.ToEntity();
            _context.ApiResources.Add(apiResourceEntity);

            await _context.SaveChangesAsync();

            return clientEntity;
        }
    }
}
