using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Application.Commands;
using SaaSEqt.IdentityAccess.Domain.Repositories;
using SaaSEqt.IdentityAccess.Domain.Services;
using SaaSEqt.IdentityAccess.Infra.Data;
using SaaSEqt.IdentityAccess.Infra.Data.Context;
using SaaSEqt.IdentityAccess.Infra.Data.Repositories;
using SaaSEqt.IdentityAccess.Infrastructure.Services;

namespace SaaSEqt.IdentityAccess.Test
{
    public interface IInt {
        void Greeting();
    }

    public class Greeter : IInt
    {
        public void Greeting()
        {
            Console.WriteLine("Hello!");
        }
    }

    public class PrivateClass {
        readonly IInt @int;

        public PrivateClass(IInt @int)
        {
            this.@int = @int;
        }
        public void Greet(){
            @int.Greeting();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<IdentityAccessDbContext>();
            optionsBuilder
                .UseMySql("Server=localhost;database=IdentityAccess;uid=root;pwd=P@ssword;oldguids=true;SslMode=None")
                .EnableSensitiveDataLogging();

            IdentityAccessDbContext context = new IdentityAccessDbContext(optionsBuilder.Options);

                //IdentityApplicationService identityApplicationService = new IdentityApplicationService();
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddScoped(s=>context)
                .AddSingleton<AuthenticationService>(_ => new AuthenticationService(
                    _.GetService<ITenantRepository>(),
                    _.GetService<IUserRepository>(),
                    _.GetService<IEncryptionService>()))
                .AddSingleton<GroupMemberService>(_ => new GroupMemberService(
                    _.GetService<IUserRepository>(),
                    _.GetService<IGroupRepository>()))
                .AddSingleton<TenantProvisioningService>(_ => new TenantProvisioningService(
                    _.GetService<ITenantRepository>(),
                    _.GetService<IUserRepository>(),
                    _.GetService<IRoleRepository>()))
                .AddSingleton<ITenantRepository, TenantRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IRoleRepository, RoleRepository>()
                .AddSingleton<IGroupRepository, GroupRepository>()
                .AddSingleton<IEncryptionService, MD5EncryptionService>()
                .AddSingleton<IdentityApplicationService>(s => new IdentityApplicationService(
                    s.GetService<AuthenticationService>(),
                    s.GetService<GroupMemberService>(),
                    s.GetService<IGroupRepository>(),
                    s.GetService<TenantProvisioningService>(),
                    s.GetService<ITenantRepository>(),
                    s.GetService<IUserRepository>()
                ))
                .AddTransient<IInt, Greeter>()
                .AddTransient<PrivateClass>()
                .BuildServiceProvider();


            using (var scope1 = serviceProvider.CreateScope())
            {
                var p = scope1.ServiceProvider;

                //IInt @int = p.GetService<IInt>();
                //@int.Greeting();

                PrivateClass privateClass = p.GetService<PrivateClass>();
                privateClass.Greet();

                TenantProvisioningService tenantProvisioningService = p.GetService<TenantProvisioningService>();


                ProvisionTenantCommand command = new ProvisionTenantCommand(
                    "abc"+Guid.NewGuid().ToString().Replace("-", ""),
                    "description",
                    "Jack"+ Guid.NewGuid().ToString(),
                    "Leung"+ Guid.NewGuid().ToString(),
                    "abc"+Guid.NewGuid().ToString().Replace("-","")+"lzhinian@me.com",
                    "123-123-1234",
                    "123-123-1234",
                    "123",
                    "123",
                    "123",
                    "123",
                    "123"
                );
                IdentityApplicationService identityApplicationService = p.GetService<IdentityApplicationService>();


                identityApplicationService.ProvisionTenant(command);

                //using (var processor = new ReservationCommandProcessor())
                //{
                //    processor.Start();

                //    Console.WriteLine("Host started");
                //    Console.WriteLine("Press enter to finish");
                //    Console.ReadLine();

                //    processor.Stop();
                //}

            }
        }
    }
}
