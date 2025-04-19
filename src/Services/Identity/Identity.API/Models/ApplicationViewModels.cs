using System.Collections.Generic;

namespace Eva.eShop.Services.Identity.API.Models
{
    public class ApiResourceViewModel
    {
        /// <summary>
        /// The unique name of the resource.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the resource.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the resource.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of accociated user claims that should be included when this resource is requested.
        /// </summary>
        public ICollection<string> UserClaims { get; set; } = new HashSet<string>();
    }

    public class ApplicationViewModel
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string HomepageURL { get; set; }

        public string AuthorizationCallbackURL { get; set; }

        public List<string> AllowedScopes { get; set; }

        public ApiResourceViewModel ApiResource { get; set; }
    }

    /// <summary>
    /// {
    ///  "secret": "secret",
    ///  "applicationName": "App Name",
    ///  "applicationDescription": "App Description",
    ///  "homepageURL": "homepage url",
    ///  "authorizationCallbackURL": "app auth callback url",
    ///  "allowedScopes": [
    ///    "payments", "name", "role"
    ///  ],
    ///  "apiResource": {
    ///    "name": "api name",
    ///    "displayName": "api display name",
    ///    "description": "api description",
    ///    "userClaims": [
    ///      "role", "name"
    ///    ]
    ///  }
    ///}
    /// </summary>
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

        public ApiResourceViewModel ApiResource { get; set; }
    }
}
