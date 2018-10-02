using System;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CqrsFramework.Events;
using CqrsFramework.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace CqrsFramework.EventStore.MySqlDB.Services
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly EventStoreDbContext _eventStoreContext;
        private readonly DbConnection _dbConnection;

        public IntegrationEventLogService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection?? throw new ArgumentNullException("dbConnection");
            _eventStoreContext = new EventStoreDbContext(
                new DbContextOptionsBuilder<EventStoreDbContext>()
                    .UseMySql(_dbConnection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options);
        }

        public Task SaveEventAsync(IEvent @event, DbTransaction transaction)
        {
            if(transaction == null)
            {
                throw new ArgumentNullException("transaction", $"A {typeof(DbTransaction).FullName} is required as a pre-requisite to save the event.");
            }
            
            var eventEntry = FromEvent(@event);

            _eventStoreContext.Events.Add(eventEntry);
            if (null == _eventStoreContext.Database.CurrentTransaction)
                _eventStoreContext.Database.UseTransaction(transaction);

            return _eventStoreContext.SaveChangesAsync();
        }

        public Task MarkEventAsPublishedAsync(IEvent @event)
        {
            var eventEntry = _eventStoreContext.Events.Single(ie => ie.AggregateId == @event.Id && ie.Version == @event.Version);
            eventEntry.Version++;
            eventEntry.State = EventStateEnum.Published;

            _eventStoreContext.Events.Update(eventEntry);

            return _eventStoreContext.SaveChangesAsync();
        }

        private Event FromEvent(IEvent @event)
        {
            var eventEntity = new Event
            {
                AggregateId = @event.Id,
                AggregateType = "",
                Version = @event.Version,
                Payload = JsonConvert.SerializeObject(@event),
                State = EventStateEnum.NotPublished,
                TimeStamp = @event.TimeStamp,
                CorrelationId = "",
                EventType = string.Format("{0}, {1}", @event.GetType().FullName, @event.GetType().GetTypeInfo().Assembly.GetName().Name)
            };

            return eventEntity;
        }
    }
}
