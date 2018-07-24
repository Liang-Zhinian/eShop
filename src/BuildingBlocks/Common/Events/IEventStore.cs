using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSEqt.Common.Domain.Model;

namespace SaaSEqt.Common.Events
{
    public interface IEventStore
    {
        long CountStoredEvents();

        StoredEvent[] GetAllStoredEventsSince(long storedEventId);

        StoredEvent[] GetAllStoredEventsBetween(long lowStoredEventId, long highStoredEventId);

        StoredEvent Append(IDomainEvent domainEvent);

        void Close();
    }
}
