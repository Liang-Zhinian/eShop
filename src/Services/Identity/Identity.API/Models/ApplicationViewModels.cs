using IdentityServer4;
using System.Collections.Generic;

namespace Eva.eShop.Services.Identity.API.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string HomepageURL { get; set; }

        public string AuthorizationCallbackURL { get; set; }
    }

    public class CreateApplicationViewModel
    {
        public string Secret { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string HomepageURL { get; set; }

        public string AuthorizationCallbackURL { get; set; }

        public List<string> AllowedScopes { get; set; } = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess
        };
    }
}
