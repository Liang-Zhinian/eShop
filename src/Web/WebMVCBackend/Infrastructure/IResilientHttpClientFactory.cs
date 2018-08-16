using SaaSEqt.eShop.BuildingBlocks.Resilience.Http;
using System;

namespace SaaSEqt.eShop.WebMVCBackend.Infrastructure
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}