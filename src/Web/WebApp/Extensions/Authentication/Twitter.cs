using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Twitter;
using System.Security.Claims;

namespace WebMVC.Extensions.Authentication
{
    public static class Twitter
    {
        public static AuthenticationBuilder AddTwitter(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {

            // You must first create an app with Twitter and add its key and Secret to your user-secrets.
            // https://apps.twitter.com/
            // https://developer.twitter.com/en/docs/basics/authentication/api-reference/access_token
            authenticationBuilder.AddTwitter(o =>
                {
                    o.ConsumerKey = configuration["twitter:consumerkey"];
                    o.ConsumerSecret = configuration["twitter:consumersecret"];
                     // http://stackoverflow.com/questions/22627083/can-we-get-email-id-from-twitter-oauth-api/32852370#32852370
                     // http://stackoverflow.com/questions/36330675/get-users-email-from-twitter-api-for-external-login-authentication-asp-net-mvc?lq=1
                     o.RetrieveUserDetails = true;
                    o.SaveTokens = true;
                    o.ClaimActions.MapJsonKey("urn:twitter:profilepicture", "profile_image_url", ClaimTypes.Uri);
                    o.Events = new TwitterEvents()
                    {
                        //OnRemoteFailure = HandleOnRemoteFailure
                    };
                });

            return authenticationBuilder;
        }
    }
}
