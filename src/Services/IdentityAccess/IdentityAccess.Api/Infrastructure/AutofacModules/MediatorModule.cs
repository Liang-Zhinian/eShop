using Autofac;
using Autofac.Core;
using FluentValidation;
using MediatR;
using SaaSEqt.IdentityAccess.Application.Behaviors;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SaaSEqt.IdentityAccess.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommand).GetTypeInfo().Assembly)
                //.AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            //builder.RegisterAssemblyTypes(typeof(TenantDomainEventHandler).GetTypeInfo().Assembly)
                //.AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            //builder
                //.RegisterAssemblyTypes(typeof(CreateOrderCommandValidator).GetTypeInfo().Assembly)
                //.Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                //.AsImplementedInterfaces();


            //builder.Register<SingleInstanceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            //});

            //builder.Register<MultiInstanceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();

            //    return t =>
            //    {
            //        var resolved = (IEnumerable<object>)componentContext.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            //        return resolved;
            //    };
            //});

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
