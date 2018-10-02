using System;
using CqrsFramework.EventStore.IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.Common.Events;
using SaaSEqt.IdentityAccess.Infra.Data.Context;

namespace SaaSEqt.IdentityAccess.Infra.Services
{
    public class MySqlEventStore : Common.Events.IEventStore
    {
        private readonly IdentityAccessDbContext _context;
        private readonly IntegrationEventLogContext _integrationEventLogContext;

        public MySqlEventStore(
            IdentityAccessDbContext context
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _integrationEventLogContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                .UseMySql(_context.Database.GetDbConnection())
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options);
        }

        public StoredEvent Append(IDomainEvent domainEvent)
        {
            return SaveIntegrationEventLogContext(domainEvent);
        }

        private StoredEvent SaveIntegrationEventLogContext(IDomainEvent domainEvent)
        {
            var eventLogEntry = new IntegrationEventLogEntry(Guid.NewGuid().ToString(),
                                                                 domainEvent.GetType().FullName,
                                                                 domainEvent.TimeStamp.DateTime,
                                                                 JsonConvert.SerializeObject(domainEvent)
                                                                );

            eventLogEntry.TimesSent++;
            eventLogEntry.State = EventStateEnum.Published;

            _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

            int result = _integrationEventLogContext.SaveChanges();

            return new StoredEvent(domainEvent.GetType().FullName, domainEvent.TimeStamp.DateTime, JsonConvert.SerializeObject(domainEvent));
        }

        public void Close()
        {
            _integrationEventLogContext.Database.CloseConnection();
        }

        public long CountStoredEvents()
        {
            throw new NotImplementedException();
        }

        public StoredEvent[] GetAllStoredEventsBetween(long lowStoredEventId, long highStoredEventId)
        {
            throw new NotImplementedException();
        }

        public StoredEvent[] GetAllStoredEventsSince(long storedEventId)
        {
            throw new NotImplementedException();
        }
    }
}
