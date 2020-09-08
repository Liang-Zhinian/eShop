using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Http;

namespace WebMVC.Extensions.Authentication
{
	public static class DmsOIDC
    {
		public static AuthenticationBuilder AddDmsOIDC(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {         
            var useLoadTest = configuration.GetValue<bool>("UseLoadTest");
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = configuration.GetValue<string>("CallBackUrl");

            // default scheme from the earlier article
            // normal OIDC login flow (via Login button or [Authorize] attrib)
            authenticationBuilder.AddOpenIdConnect("DMS", options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityUrl;
                //options.SignedOutRedirectUri = callBackUrl.ToString();

				options.ClientId = "dms.client";
                options.ClientSecret = "secret";

                options.RequireHttpsMetadata = false;
                // 使用混合流
                options.ResponseType = "code id_token";
                // 是否将Tokens保存到AuthenticationProperties中
                options.SaveTokens = true;
                // 是否从UserInfoEndpoint获取Claims
                options.GetClaimsFromUserInfoEndpoint = true;
                // 在本示例中，使用的是IdentityServer，而它的ClaimType使用的是JwtClaimTypes。
                options.TokenValidationParameters.NameClaimType = "name"; //JwtClaimTypes.Name;

                // 以下参数均有对应的默认值，通常无需设置。
                //o.CallbackPath = new PathString("/signin-oidc");
                //o.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
                //o.RemoteSignOutPath = new PathString("/signout-oidc");
                //o.Scope.Add("openid");
                //o.Scope.Add("profile");
                //o.ResponseMode = OpenIdConnectResponseMode.FormPost; 

                //options.Scope.Clear();
                //options.Scope.Add("openid");
                //options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("offline_access");
                //options.Scope.Add("rabbit_metrics.realestate");
                //options.Scope.Add("rabbit_metrics.rental");
                //options.Scope.Add("rabbit_metrics.sites");
                //options.Scope.Add("rabbit_metrics.identityaccess");
                ////options.Scope.Add("rabbit_metrics.webagg");

                options.ClaimActions.Remove("amr");
                options.ClaimActions.MapUniqueJsonKey("updated_at", "updated_at");
                options.ClaimActions.MapUniqueJsonKey("locale", "locale");
                options.ClaimActions.MapUniqueJsonKey("zoneinfo", "zoneinfo");
                options.ClaimActions.MapUniqueJsonKey("birthdate", "birthdate");
                options.ClaimActions.MapUniqueJsonKey("gender", "gender");
                options.ClaimActions.MapUniqueJsonKey("profile", "profile");
                options.ClaimActions.MapJsonKey("website", "website");
                options.ClaimActions.MapUniqueJsonKey("picture", "picture");
                options.ClaimActions.MapUniqueJsonKey("preferred_username", "preferred_username");
                options.ClaimActions.MapUniqueJsonKey("nickname", "nickname");
                options.ClaimActions.MapUniqueJsonKey("middle_name", "middle_name");
                options.ClaimActions.MapUniqueJsonKey("given_name", "given_name");
                options.ClaimActions.MapUniqueJsonKey("family_name", "family_name");
                options.ClaimActions.MapUniqueJsonKey("name", "name");


                /***********************************相关事件***********************************/
                // 未授权时，重定向到OIDC服务器时触发
                //o.Events.OnRedirectToIdentityProvider = context => Task.CompletedTask;

                // 获取到授权码时触发
                //o.Events.OnAuthorizationCodeReceived = context => Task.CompletedTask;
                // 接收到OIDC服务器返回的认证信息（包含Code, ID Token等）时触发
                //o.Events.OnMessageReceived = context => Task.CompletedTask;
                // 接收到TokenEndpoint返回的信息时触发
                //o.Events.OnTokenResponseReceived = context => Task.CompletedTask;
                // 验证Token时触发
                //o.Events.OnTokenValidated = context => Task.CompletedTask;
                // 接收到UserInfoEndpoint返回的信息时触发
                //o.Events.OnUserInformationReceived = context => Task.CompletedTask;
                // 出现异常时触发
                //o.Events.OnAuthenticationFailed = context => Task.CompletedTask;

                // 退出时，重定向到OIDC服务器时触发
                //o.Events.OnRedirectToIdentityProviderForSignOut = context => Task.CompletedTask;
                // OIDC服务器退出后，服务端回调时触发
                //o.Events.OnRemoteSignOut = context => Task.CompletedTask;
                // OIDC服务器退出后，客户端重定向时触发
                //o.Events.OnSignedOutCallbackRedirect = context => Task.CompletedTask;

                options.Events.OnUserInformationReceived = ctx =>
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
