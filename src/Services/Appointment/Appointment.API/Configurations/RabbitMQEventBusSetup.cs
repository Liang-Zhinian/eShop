using Autofac;
using CqrsFramework.Bus;
using CqrsFramework.Bus.RabbitMQ;
using CqrsFramework.Commands;
using CqrsFramework.Events;
using CqrsFramework.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Appointment.API.Configurations
{
    public static class RabbitMQEventBusSetup
    {
        public static IConfiguration Configuration { get; private set; }

        public static void AddRabbitMQEventBusSetup(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;

            RegisterEventBus(services);

            //return services;
        }

        private static void RegisterEventBus(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBusConnection"]
                };

                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                {
                    factory.UserName = Configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                {
                    factory.Password = Configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });


            var subscriptionClientName = Configuration["SubscriptionClientName"];

            services.AddSingleton<RabbitMQBus>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<RabbitMQBus>>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new RabbitMQBus(rabbitMQPersistentConnection, logger, iLifetimeScope, "book2_event_bus", "fanout", subscriptionClientName, false, retryCount);
            });

            //services.AddSingleton<ICommandSender>(y => y.GetService<RabbitMQBus>());
            services.AddSingleton<IEventPublisher>(y => y.GetService<RabbitMQBus>());
            services.AddSingleton<IHandlerRegistrar>(y => y.GetService<RabbitMQBus>());


        }
    }
}
