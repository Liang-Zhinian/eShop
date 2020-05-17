using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eva.eShop.Services.Locations.API.Infrastructure.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
