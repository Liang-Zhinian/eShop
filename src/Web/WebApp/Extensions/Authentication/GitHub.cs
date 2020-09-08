using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WebMVC.Extensions.Authentication
{
    public static class GitHub
    {
        public static AuthenticationBuilder AddGitHub(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {
            // You must first create an app with GitHub and add its ID and Secret to your user-secrets.
            // https://github.com/settings/applications/
            authenticationBuilder
                /*.AddOAuth("GitHub-AccessToken", "GitHub AccessToken only", o =>
                 {
                     o.ClientId = configuration["github-token:clientid"];
                     o.ClientSecret = configuration["github-token:clientsecret"];
                     o.CallbackPath = new PathString("/signin-github-token");
                     o.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                     o.TokenEndpoint = "https://github.com/login/oauth/access_token";
                     o.SaveTokens = true;
                     o.Events = new OAuthEvents()
                     {
                        OnRemoteFailure = AuthenticationExtensions.HandleOnRemoteFailure
                     };
                 })*/
                // You must first create an app with GitHub and add its ID and Secret to your user-secrets.
                // https://github.com/settings/applications/
                .AddOAuth("GitHub", "Github", o =>
                {
                    o.ClientId = configuration["github:clientid"];
                    o.ClientSecret = configuration["github:clientsecret"];
                    o.CallbackPath = new PathString("/signin-github");
                    o.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                    o.TokenEndpoint = "https://github.com/login/oauth/access_token";
                    o.UserInformationEndpoint = "https://api.github.com/user";
                    o.ClaimsIssuer = "OAuth2-Github";
                    o.SaveTokens = true;
                    // Retrieving user information is unique to each provider.
                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
                    o.ClaimActions.MapJsonKey("urn:github:name", "name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);
                    o.ClaimActions.MapJsonKey("urn:github:url", "url");
                    o.Events = new OAuthEvents
                    {
                        //OnRemoteFailure = HandleOnRemoteFailure,
                        OnCreatingTicket = async context =>
                        {
                            // Get the GitHub user
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                            context.RunClaimActions(user);
                        }
                    };
                });

            return authenticationBuilder;
        }
    }
}
