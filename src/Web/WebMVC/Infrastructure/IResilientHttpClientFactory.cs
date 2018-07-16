using SaaSEqt.eShop.BuildingBlocks.Resilience.Http;
using System;

namespace SaaSEqt.eShop.WebMVC.Infrastructure
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}