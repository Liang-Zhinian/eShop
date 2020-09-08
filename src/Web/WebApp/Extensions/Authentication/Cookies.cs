using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace WebMVC.Extensions.Authentication
{
    public static class Cookies
    {
        public static AuthenticationBuilder AddCookies(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {
            

            return authenticationBuilder;
        }
    }
}
