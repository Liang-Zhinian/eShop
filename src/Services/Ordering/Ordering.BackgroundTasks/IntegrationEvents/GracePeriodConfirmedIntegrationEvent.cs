﻿using Eva.BuildingBlocks.EventBus.Events;

namespace Ordering.BackgroundTasks.IntegrationEvents
{
    public class GracePeriodConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public GracePeriodConfirmedIntegrationEvent(int orderId) =>
            OrderId = orderId;
    }
}
