using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SaaSEqt.eShop.BuildingBlocks.EventBus;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.BuildingBlocks.EventBusRabbitMQ;
using SaaSEqt.eShop.BuildingBlocks.EventBusServiceBus;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.Locations;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.ServiceCatalogs;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling.Sites;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Locations;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.ServiceCatalogs;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Sites;

namespace SaaSEqt.eShop.Services.Sites.API.Configurations
{

    public static class EventBusSetup
    {
        public static IConfiguration Configuration { get; private set; }

        public static IServiceCollection AddEventBusSetup(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;

            SetupEventBus(services);

            return services;
        }

        private static void SetupEventBus(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<BusinessSettings>>().Value;
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
        }


        public static void RegisterEventBus(IServiceCollection services)
        {
            var subscriptionClientName = Configuration["SubscriptionClientName"];

            if (Configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                });

            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                    }

                    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<SiteCreatedEventHandler>();
            // locations
            services.AddTransient<LocationCreatedEventHandler>();
            services.AddTransient<AdditionalLocationImageCreatedEventHandler>();
            services.AddTransient<LocationPostalAddressChangedEventHandler>();
            services.AddTransient<LocationGeolocationChangedEventHandler>();
            services.AddTransient<LocationImageChangedEventHandler>();
            services.AddTransient<LocationContactInformationChangedEventHandler>();

            // catalogs
            services.AddTransient<ServiceCategoryCreatedEventHandler>();
            services.AddTransient<ServiceItemCreatedEventHandler>();
        }

        public static void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<SiteCreatedEvent, SiteCreatedEventHandler>();

            // locations
            eventBus.Subscribe<LocationCreatedEvent, LocationCreatedEventHandler>();
            eventBus.Subscribe<AdditionalLocationImageCreatedEvent, AdditionalLocationImageCreatedEventHandler>();
            eventBus.Subscribe<LocationAddressChangedEvent, LocationPostalAddressChangedEventHandler>();
            eventBus.Subscribe<LocationGeolocationChangedEvent, LocationGeolocationChangedEventHandler>();
            eventBus.Subscribe<LocationImageChangedEvent, LocationImageChangedEventHandler>();
            eventBus.Subscribe<LocationContactInformationChangedEvent, LocationContactInformationChangedEventHandler>();


            // catalogs
            eventBus.Subscribe<ServiceCategoryCreatedEvent, ServiceCategoryCreatedEventHandler>();
            eventBus.Subscribe<ServiceItemCreatedEvent, ServiceItemCreatedEventHandler>();

        }
    }
}
