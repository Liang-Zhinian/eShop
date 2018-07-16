using Autofac;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Ordering.API.Application.Commands;
using SaaSEqt.eShop.Services.Ordering.API.Application.Queries;
using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
using SaaSEqt.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using SaaSEqt.eShop.Services.Ordering.Infrastructure.Idempotency;
using SaaSEqt.eShop.Services.Ordering.Infrastructure.Repositories;
using System.Reflection;

namespace SaaSEqt.eShop.Services.Ordering.API.Infrastructure.AutofacModules
{

    public class ApplicationModule
        :Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new OrderQueries(QueriesConnectionString))
                .As<IOrderQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BuyerRepository>()
                .As<IBuyerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
