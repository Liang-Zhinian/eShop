using Microsoft.Extensions.DependencyInjection;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.eShop.Business.API.Infrastructure.Services;
using SaaSEqt.IdentityAccess;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Services;
using SaaSEqt.IdentityAccess.Infrastructure.Context;
using SaaSEqt.IdentityAccess.Infrastructure.Services;

namespace SaaSEqt.eShop.Business.API.Configurations
{
    public static class ApplicationSetup
    {
        public static void AddApplicationSetup(this IServiceCollection services)
        {
            RegisterIdentityAccessRepositories(services);

            RegisterIdentityAccessServices(services);

            // Infra - Data
            RegisterWriteDb(services);

            RegisterAppService(services);

        }

        private static void RegisterAppService(IServiceCollection services)
        {
            // App service
            services.AddScoped<ITenantService, TenantService>();
        }

        private static void RegisterWriteDb(IServiceCollection services)
        {
            services.AddScoped<IdentityAccessDbContext>();
        }

        private static void RegisterIdentityAccessRepositories(IServiceCollection services)
        {
            //services
                //.AddScoped<ITenantRepository, TenantRepository>()
                //.AddScoped<IUserRepository, UserRepository>()
                //.AddScoped<IRoleRepository, RoleRepository>()
                //.AddScoped<IGroupRepository, GroupRepository>();
        }

        private static void RegisterIdentityAccessServices(IServiceCollection services)
        {
            services
                .AddTransient<AuthenticationService>()
                .AddTransient<GroupMemberService>()
                .AddTransient<TenantProvisioningService>()
                //.AddScoped<ITenantRepository, TenantRepository>()
                //.AddScoped<IUserRepository, UserRepository>()
                //.AddScoped<IRoleRepository, RoleRepository>()
                //.AddScoped<IGroupRepository, GroupRepository>()
                .AddTransient<IEncryptionService, MD5EncryptionService>()
                .AddTransient<IdentityApplicationService>()
                .AddSingleton<DomainEventPublisher>();
        }
    }

    public static class IdentityAccessEventProcessorSetup
    {
        public static void AddIdentityAccessEventProcessorSetup(this IServiceCollection services)
        {
            //services
                //.AddScoped<SaaSEqt.Common.Events.IEventStore, SaaSEqt.IdentityAccess.Infra.Services.MySqlEventStore>()
                //.AddScoped<IdentityAccessEventProcessor>();
        }
    }
}
