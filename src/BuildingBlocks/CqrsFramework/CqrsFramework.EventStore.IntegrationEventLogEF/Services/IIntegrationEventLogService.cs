using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using CqrsFramework.Events;

namespace CqrsFramework.EventStore.IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IEvent @event, DbTransaction transaction);
        Task MarkEventAsPublishedAsync(IEvent @event);
        int SaveEvent(IEvent @event, DbTransaction transaction);
        int MarkEventAsPublished(IEvent @event);
    }
}
