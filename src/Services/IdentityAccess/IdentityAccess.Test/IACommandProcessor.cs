using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Domain.Repositories;
using SaaSEqt.IdentityAccess.Infra.Data;
using SaaSEqt.IdentityAccess.Infra.Data.Context;
using SaaSEqt.IdentityAccess.Infra.Data.Repositories;

namespace SaaSEqt.IdentityAccess.Test
{
    public class ReservationCommandProcessor : IDisposable
    {
        private ServiceProvider serviceProvider;

        public ReservationCommandProcessor()
        {
            this.serviceProvider = CreateServiceProvider();
            RegisterHandlers(serviceProvider);
        }

        public void Start()
        {
            
        }

        public void Stop()
        { 
        }

        public void Dispose()
        {
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityAccessDbContext>();
            optionsBuilder.UseMySql("Server=localhost;database=IdentityAccess;uid=root;pwd=P@ssword;oldguids=true;SslMode=None");

            IdentityAccessDbContext context = new IdentityAccessDbContext(optionsBuilder.Options);
            
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddDbContext<IdentityAccessDbContext>(ServiceLifetime.Scoped)
                .AddScoped<ITenantRepository, TenantRepository>() 
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IGroupRepository, GroupRepository>()           
                .BuildServiceProvider();



        //configure console logging

            return serviceProvider;
        }


        private static void RegisterHandlers(ServiceProvider serviceProvider)
        {
        }
    }
}
