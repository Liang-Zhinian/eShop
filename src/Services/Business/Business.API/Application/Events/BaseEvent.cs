using CqrsFramework.Events;
using System;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events
{
    public class BaseEvent
    {
        /// <summary>
        /// The ID of the Aggregate being affected by this event
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Version of the Aggregate which results from this event
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The UTC time when this event occurred.
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }
        public string MessageType { get; set; }
    }
}
