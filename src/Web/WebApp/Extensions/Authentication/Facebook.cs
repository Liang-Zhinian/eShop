using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace WebMVC.Extensions.Authentication
{
    public static class Facebook
    {
        public static AuthenticationBuilder AddFacebook(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {

            if (string.IsNullOrEmpty(configuration["facebook:appid"]))
            {
                // User-Secrets: https://docs.asp.net/en/latest/security/app-secrets.html
                // See below for registration instructions for each provider.
                throw new InvalidOperationException("User secrets must be configured for each authentication provider.");
            }

            // You must first create an app with Facebook and add its ID and Secret to your user-secrets.
            // https://developers.facebook.com/apps/
            // https://developers.facebook.com/docs/facebook-login/manually-build-a-login-flow#login
            authenticationBuilder.AddFacebook(o =>
                 {
                     o.AppId = configuration["facebook:appid"];
                     o.AppSecret = configuration["facebook:appsecret"];
                     o.Scope.Add("email");
                     o.Fields.Add("name");
                     o.Fields.Add("email");
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
