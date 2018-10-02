using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CqrsFramework.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace CqrsFramework.EventStore.IntegrationEventLogEF
{
    public class EfMySqlEventStore : IEventStore
    {

        private readonly IntegrationEventLogContext _integrationEventLogContext;
        private readonly DbConnection _dbConnection;

        public EfMySqlEventStore(DbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException("dbConnection");
            _integrationEventLogContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseMySql(_dbConnection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options);
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            IList<IEvent> events = new List<IEvent>();
            var evts = _integrationEventLogContext.IntegrationEventLogs.Where(_=>_.AggregateId.Equals(aggregateId));
            foreach (var evt in evts)
            {
                var eventTypeString = evt.EventTypeName;
                var eventType = Type.GetType(eventTypeString);
                var serializedBody = evt.Content;
                var @event = JsonConvert.DeserializeObject(serializedBody, eventType);
                events.Add((IEvent)@event);
            }

            return Task.FromResult(events.AsEnumerable());
        }

        public Task Save(IEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            var eventLogEntry = new IntegrationEventLogEntry(@event);

            _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);
            _integrationEventLogContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach(var @event in events)
            {
                var eventLogEntry = new IntegrationEventLogEntry(@event);

                _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);
            };

            _integrationEventLogContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
