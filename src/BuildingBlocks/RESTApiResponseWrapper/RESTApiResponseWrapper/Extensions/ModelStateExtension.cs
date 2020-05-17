using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using Eva.BuildingBlocks.RESTApiResponseWrapper.Wrappers;

namespace Eva.BuildingBlocks.RESTApiResponseWrapper.Extensions
{
    public static class ModelStateExtension
    {
        public static IEnumerable<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList();
        }
    }

}
