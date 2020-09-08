using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace WebMVC.Extensions.Authentication
{
    public static class OAuth
    {
        public static AuthenticationBuilder AddOAuth(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {

            var useLoadTest = configuration.GetValue<bool>("UseLoadTest");
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = configuration.GetValue<string>("CallBackUrl");

            authenticationBuilder.AddOAuth("AuthHub", "AuthHub AccessToken only", o =>
            {
                o.ClientId = "mvc";
                o.ClientSecret = "secret";
                o.CallbackPath = new PathString("/signin-oidc");
                o.AuthorizationEndpoint = $"{identityUrl}/connect/authorize";
                o.TokenEndpoint = $"{identityUrl}/connect/token";
                o.Scope.Add("openid");
                o.Scope.Add("profile");
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
