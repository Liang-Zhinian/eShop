using Microsoft.AspNetCore.Builder;
using System;

namespace Eva.BuildingBlocks.RESTApiResponseWrapper
{
    public static class ApiResponseMiddlewareExtension
    {
        public static IApplicationBuilder UseApiResponseAndExceptionWrapper(this IApplicationBuilder builder, ApiResponseOptions options = default(ApiResponseOptions))
        {
            options = options ?? new ApiResponseOptions();
            return builder.UseMiddleware<ApiResponseMiddleware>(options);
        }
    }
}
