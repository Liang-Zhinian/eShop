using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Services;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Utilities;
using SaaSEqt.eShop.Services.Business.Infrastructure.Data;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly BusinessDbContext _context;
        private readonly IIntegrationEventLogService _eventLogService;

        public CatalogIntegrationEventService(IEventBus eventBus, BusinessDbContext catalogContext,
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _context = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_context.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {

            await SaveEventAndCatalogContextChangesAsync(evt);

            _eventBus.Publish(evt);

            await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        public async Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_context)
                .ExecuteAsync(async () => {
                    // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                    await _context.SaveChangesAsync();
                    await _eventLogService.SaveEventAsync(evt, _context.Database.CurrentTransaction.GetDbTransaction());

                });
        }
    }
}
