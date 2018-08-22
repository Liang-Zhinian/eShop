using Autofac;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Domain.Model.Access.Repositories;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Repositories;
using SaaSEqt.IdentityAccess.Infrastructure.Repositories;
using SaaSEqt.IdentityAccess.Infrastructure.UoW;
using System;

namespace SaaSEqt.eShop.Business.API.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        //public string QueriesConnectionString { get; }

        public ApplicationModule(/*string qconstr*/)
        {
            //QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            //builder.Register(c => new OrderQueries(QueriesConnectionString))
                //.As<IOrderQueries>()
                //.InstancePerLifetimeScope();

            builder.RegisterType<GroupRepository>()
                   .As<IGroupRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<RoleRepository>()
                   .As<IRoleRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                   .As<IUserRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<TenantRepository>()
                   .As<ITenantRepository>()
                   .InstancePerLifetimeScope();
            
            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            

            //builder.RegisterType<RequestManager>()
            //   .As<IRequestManager>()
            //   .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
                //.AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
