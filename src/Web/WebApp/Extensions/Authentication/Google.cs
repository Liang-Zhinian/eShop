using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMVC.Extensions.Authentication;
using System.Security.Claims;

namespace WebMVC.Extensions.Authentication
{
    public static class Google
    {
        public static AuthenticationBuilder AddGoogle(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {

            // You must first create an app with Google and add its ID and Secret to your user-secrets.
            // https://console.developers.google.com/project
            // https://developers.google.com/identity/protocols/OAuth2WebServer
            // https://developers.google.com/+/web/people/
            authenticationBuilder.AddGoogle(o =>
                {
                    o.ClientId = configuration["google:clientid"];
                    o.ClientSecret = configuration["google:clientsecret"];
                    o.AuthorizationEndpoint += "?prompt=consent"; // Hack so we always get a refresh token, it only comes on the first authorization response
                     o.AccessType = "offline";
                    o.SaveTokens = true;
                    o.Events = new OAuthEvents()
                    {
                        //OnRemoteFailure = HandleOnRemoteFailure
                    };
                    o.ClaimActions.MapJsonSubKey("urn:google:image", "image", "url");
                    o.ClaimActions.Remove(ClaimTypes.GivenName);
                })

           // You must first create an app with Google and add its ID and Secret to your user-secrets.
           // https://console.developers.google.com/project
           // https://developers.google.com/identity/protocols/OAuth2WebServer
           // https://developers.google.com/+/web/people/
           .AddOAuth("Google-AccessToken", "Google AccessToken only", o =>
            {
                o.ClientId = configuration["google:clientid"];
                o.ClientSecret = configuration["google:clientsecret"];
                o.CallbackPath = new PathString("/signin-google-token");
                o.AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint;
                o.TokenEndpoint = GoogleDefaults.TokenEndpoint;
                o.Scope.Add("openid");
                o.Scope.Add("profile");
                o.Scope.Add("email");
                o.SaveTokens = true;
                o.Events = new OAuthEvents()
                {
                    //OnRemoteFailure = HandleOnRemoteFailure
                };
            });

            return authenticationBuilder;
        }
    }
}
