using System;
using CqrsFramework.Events;
using MediatR;

namespace CqrsFramework.Domain
{
    public class Notification : IEvent, INotification
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; set; }

        public Guid Id { get; set; }
        public DateTimeOffset TimeStamp{ get; set; }
        public string MessageType { get; set; }

        public Notification(string key, string value)
        {
            Id = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
            TimeStamp = DateTime.Now;
            MessageType = GetType().FullName;
        }
    }
}
