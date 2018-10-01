using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Services;
using SaaSEqt.eShop.BuildingBlocks.IntegrationEventLogEF.Utilities;
using SaaSEqt.eShop.Services.Schedule.API.Infrastructure;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Schedule.API.IntegrationEvents
{
    public class ScheduleIntegrationEventService : IScheduleIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly ScheduleContext _scheduleContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public ScheduleIntegrationEventService(IEventBus eventBus, ScheduleContext scheduleontext,
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _scheduleContext = scheduleontext ?? throw new ArgumentNullException(nameof(scheduleontext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_scheduleContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);

            await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        public async Task SaveEventAndScheduleContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_scheduleContext)
                .ExecuteAsync(async () => {
                // Achieving atomicity between original schedule database operation and the IntegrationEventLog thanks to a local transaction
                    await _scheduleContext.SaveChangesAsync();
                    await _eventLogService.SaveEventAsync(evt, _scheduleContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }
}
