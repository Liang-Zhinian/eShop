using System;
using System.Collections.Generic;
using Identity.API.Models;
using Identity.API.Configuration;

namespace Identity.API.Infrastucture.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        public ProviderRepository()
        {
        }

        public IEnumerable<Provider> Get()
        {
            return ProviderDataSource.GetProviders();
        }
    }
}
