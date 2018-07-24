using System;
using CqrsFramework.Events;

namespace SaaSEqt.IdentityAccess.API.Events
{
    public class TenantCreatedEvent: TenantEvent, IEvent
    {
        public TenantCreatedEvent(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Version = 1;
            TimeStamp = DateTimeOffset.Now;
        }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
