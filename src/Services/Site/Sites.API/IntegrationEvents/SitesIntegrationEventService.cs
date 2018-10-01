using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Services;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Utilities;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Sites.API.IntegrationEvents
{
    public class SitesIntegrationEventService : ISitesIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly SitesContext _sitesContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public SitesIntegrationEventService(IEventBus eventBus, SitesContext sitesontext,
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _sitesContext = sitesontext ?? throw new ArgumentNullException(nameof(sitesontext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_sitesContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);

            await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        public async Task SaveEventAndSitesContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_sitesContext)
                .ExecuteAsync(async () => {
                // Achieving atomicity between original sites database operation and the IntegrationEventLog thanks to a local transaction
                    await _sitesContext.SaveChangesAsync();
                    await _eventLogService.SaveEventAsync(evt, _sitesContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }
}
