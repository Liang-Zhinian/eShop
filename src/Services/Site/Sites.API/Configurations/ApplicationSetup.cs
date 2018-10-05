using Microsoft.Extensions.DependencyInjection;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure.Services;
using SaaSEqt.IdentityAccess;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Services;
using SaaSEqt.IdentityAccess.Infrastructure.Context;
using SaaSEqt.IdentityAccess.Infrastructure.Services;
using Sites.API.IntegrationEvents;

namespace SaaSEqt.eShop.Services.Sites.API.Configurations
{
    public static class ApplicationSetup
    {
        public static void AddApplicationSetup(this IServiceCollection services)
        {
            AddIntegrationEventServices(services);

            AddDomainServices(services);

        }

        private static void AddIntegrationEventServices(IServiceCollection services)
        {
            //services.AddTransient<IIdentityAccessIntegrationEventService, IdentityAccessIntegrationEventService>();

            services.AddTransient<ISitesIntegrationEventService, SitesIntegrationEventService>();
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddTransient<BusinessService>();
        }
    }
}
