using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMVC.Extensions.Authentication;


namespace WebMVC.Extensions.Authentication
{
    public static class MicrosoftAccount
    {
        public static AuthenticationBuilder AddMicrosoftAccount(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {

            /* Azure AD app model v2 has restrictions that prevent the use of plain HTTP for redirect URLs.
               Therefore, to authenticate through microsoft accounts, tryout the sample using the following URL:
               https://localhost:44318/
            */
            // You must first create an app with Microsoft Account and add its ID and Secret to your user-secrets.
            // https://apps.dev.microsoft.com/
            authenticationBuilder
                 /*.AddOAuth("Microsoft-AccessToken", "Microsoft AccessToken only", options =>
                 {
                     options.ClientId = configuration["microsoftaccount:clientid"];
                     options.ClientSecret = configuration["microsoftaccount:clientsecret"];
                     options.CallbackPath = new PathString("/signin-microsoft-token");
                     options.AuthorizationEndpoint = MicrosoftAccountDefaults.AuthorizationEndpoint;
                     options.TokenEndpoint = MicrosoftAccountDefaults.TokenEndpoint;
                     options.Scope.Add("https://graph.microsoft.com/user.read");
                     options.SaveTokens = true;
                     options.Events = new OAuthEvents()
                     {
                         //OnRemoteFailure = HandleOnRemoteFailure
                     };
                     options.ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
                     options.Events.OnCreatingTicket = ctx =>
                     {
                         List<AuthenticationToken> tokens = ctx.Properties.GetTokens()
                             as List<AuthenticationToken>;
                         tokens.Add(new AuthenticationToken()
                         {
                             Name = "TicketCreated",
                             Value = DateTime.UtcNow.ToString()
                         });
                         ctx.Properties.StoreTokens(tokens);
                         return Task.CompletedTask;
                     };
                 })*/
                                 // You must first create an app with Microsoft Account and add its ID and Secret to your user-secrets.
                                 // https://azure.microsoft.com/en-us/documentation/articles/active-directory-v2-app-registration/
                 .AddMicrosoftAccount(options =>
                {
                    options.ClientId = configuration["microsoftaccount:clientid"];
                    options.ClientSecret = configuration["microsoftaccount:clientsecret"];
                    options.SaveTokens = true;
                    options.Scope.Add("offline_access");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Gender, "gender");
                    options.Events.OnCreatingTicket = ctx =>
                    {
                        List<AuthenticationToken> tokens = ctx.Properties.GetTokens()
                            as List<AuthenticationToken>;
                        tokens.Add(new AuthenticationToken()
                        {
                            Name = "TicketCreated",
                            Value = DateTime.UtcNow.ToString()
                        });
                        ctx.Properties.StoreTokens(tokens);
                        return Task.CompletedTask;
                    };
                });

            return authenticationBuilder;
        }
    }
}
