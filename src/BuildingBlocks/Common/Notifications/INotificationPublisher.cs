using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaaSEqt.Common.Notifications
{
    public interface INotificationPublisher
    {
        void PublishNotifications();

        bool InternalOnlyTestConfirmation();
    }
}
