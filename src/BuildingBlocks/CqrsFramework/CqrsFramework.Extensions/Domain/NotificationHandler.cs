using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CqrsFramework.Domain
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        private List<Notification> _notifications;

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public Task Handle(Notification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);

            return Task.CompletedTask;
        }

        public virtual List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            _notifications = new List<Notification>();
        }
    }
}
