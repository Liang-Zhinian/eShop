using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Services;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Utilities;
using SaaSEqt.IdentityAccess.Infrastructure.Context;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents
{
    public class IdentityAccessIntegrationEventService : IIdentityAccessIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly IdentityAccessDbContext _context;
        private readonly IIntegrationEventLogService _eventLogService;

        public IdentityAccessIntegrationEventService(IEventBus eventBus, 
                                                     IdentityAccessDbContext context,
                                       Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_context.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);

            await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        public async Task SaveEventAndContextChangesAsync(IntegrationEvent evt)
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
