using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework.EventStore.IntegrationEventLogEF
{
    public enum EventStateEnum
    {
        NotPublished = 0,
        Published = 1,
        PublishedFailed = 2
    }
}
