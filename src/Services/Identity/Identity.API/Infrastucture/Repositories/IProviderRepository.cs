using System;
using System.Collections.Generic;
using Identity.API.Models;

namespace Identity.API.Infrastucture.Repositories
{
    public interface IProviderRepository
    {
        IEnumerable<Provider> Get();
    }
}
