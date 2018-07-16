using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CqrsFramework.Events;
using CqrsFramework.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace CqrsFramework.EventStore.MySqlDB
{
    public class MySqlEventStore : IEventStore
    {
        private readonly EventStoreDbContext _eventStoreContext;
        private readonly DbConnection _dbConnection;
        private readonly IEventPublisher _publisher;

        public MySqlEventStore(DbConnection dbConnection, IEventPublisher publisher)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException("dbConnection");
            _eventStoreContext = new EventStoreDbContext(
                new DbContextOptionsBuilder<EventStoreDbContext>()
                    .UseMySql(_dbConnection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options);

            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            IList<IEvent> events = new List<IEvent>();
            var evts = _eventStoreContext.Events.Where(_ => _.AggregateId.Equals(aggregateId) && _.Version > fromVersion)
                                         .OrderBy(x => x.Version)
                                            .AsEnumerable();
            foreach (var evt in evts)
            {
                var eventTypeString = evt.EventType;
                var eventType = Type.GetType(eventTypeString);
                var serializedBody = evt.Payload;
                var @event = JsonConvert.DeserializeObject(serializedBody, eventType);
                events.Add((IEvent)@event);
            }

            return Task.FromResult(events.AsEnumerable());
        }

        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var @event in events)
            {
                var eventEntity = FromEvent(@event);

                _eventStoreContext.Events.Add(eventEntity);

                await _publisher.Publish(@event, cancellationToken);
                //eventEntity.Version++;
                eventEntity.State = EventStateEnum.Published;

                //eventEntity.State = EventStateEnum.Published;
                //eventEntity.Version++;
            };

            await _eventStoreContext.SaveChangesAsync();
        }

        private Event FromEvent(IEvent @event){
            var eventEntity = new Event
            {
                AggregateId = @event.Id,
                AggregateType = "",
                Version = @event.Version,
                Payload = JsonConvert.SerializeObject(@event),
                CorrelationId = "",
                State = EventStateEnum.NotPublished,
                TimeStamp = @event.TimeStamp,
                EventType = string.Format("{0}, {1}", @event.GetType().FullName, @event.GetType().GetTypeInfo().Assembly.GetName().Name)
            };

            return eventEntity;
        }
    }
}
