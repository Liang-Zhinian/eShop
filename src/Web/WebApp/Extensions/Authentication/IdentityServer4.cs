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
    public static class IdentityServer4
    {
        public static AuthenticationBuilder AddIdentityServer4(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {
            var useLoadTest = configuration.GetValue<bool>("UseLoadTest");
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = configuration.GetValue<string>("CallBackUrl");

            // You must first create an app with GitHub and add its ID and Secret to your user-secrets.
            // https://github.com/settings/applications/
            authenticationBuilder
                .AddOAuth("IdentityServer4", "IdentityServer4", o =>
                {
                    o.ClientId = "mvc";
                    o.ClientSecret = "secret";
                    o.CallbackPath = new PathString("/signin-oidc");
                    o.AuthorizationEndpoint = $"{identityUrl}/connect/authorize";
                    o.TokenEndpoint = $"{identityUrl}/connect/token";
                    o.UserInformationEndpoint = $"{identityUrl}/connect/userinfo";
                    o.ClaimsIssuer = "OAuth2-Idsr4";
                    o.SaveTokens = true;
                    o.Scope.Clear();
                    o.Scope.Add("openid");
                    o.Scope.Add("profile");
                    //o.Scope.Add("email");
                    // Retrieving user information is unique to each provider.
                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    //o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);
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
