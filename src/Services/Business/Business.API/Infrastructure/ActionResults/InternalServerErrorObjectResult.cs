using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SaaSEqt.eShop.Services.Business.API.Infrastructure.ActionResults
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
