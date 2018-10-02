using System;
using System.Data.Common;
using System.Threading.Tasks;
using CqrsFramework.Events;
using CqrsFramework.EventStore.MySqlDB.Services;
using CqrsFramework.EventStore.MySqlDB.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
//using SaaSEqt.IdentityAccess.Infrastructure.Context;

namespace SaaSEqt.IdentityAccess.Application
{
    public class IdentityAccessIntegrationEventService : IIdentityAccessIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventPublisher _eventBus;
        //private readonly IdentityAccessDbContext _context;
        private readonly IIntegrationEventLogService _eventLogService;

        public IdentityAccessIntegrationEventService(IEventPublisher eventBus, 
                                                     //IdentityAccessDbContext context,
                                       Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            //_context = context ?? throw new ArgumentNullException(nameof(context));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            //_eventLogService = _integrationEventLogServiceFactory(_context.Database.GetDbConnection());
        }

        public Task PublishThroughEventBusAsync(IEvent evt)
        {
            Task[] tasks = new Task[3];
            //tasks[0] = _eventLogService.SaveEventAsync(evt, _context.Database.CurrentTransaction.GetDbTransaction());
            tasks[1] = _eventBus.Publish(evt);
            tasks[2] = _eventLogService.MarkEventAsPublishedAsync(evt);

            return Task.WhenAll(tasks);
        }
    }
}
