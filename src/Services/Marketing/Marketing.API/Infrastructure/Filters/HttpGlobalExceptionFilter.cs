﻿namespace Eva.eShop.Services.Marketing.API.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using global::Eva.eShop.Services.Marketing.API.Infrastructure.Exceptions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Eva.eShop.Services.Marketing.API.Infrastructure.ActionResults;
    using Microsoft.Extensions.Logging;
    using System.Net;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(MarketingDomainException)) 
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { context.Exception.Message }
                };

                // Result asigned to a result object but in destiny the response is empty. This is a known bug of .net core 1.1
                //It will be fixed in .net core 1.1.2. See https://github.com/aspnet/Mvc/issues/5594 for more information
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occur.Try it again." }
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                // Result asigned to a result object but in destiny the response is empty. This is a known bug of .net core 1.1
                // It will be fixed in .net core 1.1.2. See https://github.com/aspnet/Mvc/issues/5594 for more information
                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;                
            }
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}