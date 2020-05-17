using Autofac;
using Eva.BuildingBlocks.EventBus.Abstractions;
using Eva.eShop.Services.Ordering.API.Application.Commands;
using Eva.eShop.Services.Ordering.API.Application.Queries;
using Eva.eShop.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
using Eva.eShop.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using Eva.eShop.Services.Ordering.Infrastructure.Idempotency;
using Eva.eShop.Services.Ordering.Infrastructure.Repositories;
using System.Reflection;

namespace Eva.eShop.Services.Ordering.API.Infrastructure.AutofacModules
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
