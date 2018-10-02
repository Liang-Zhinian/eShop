using System;
using CqrsFramework.Commands;
using CqrsFramework.Domain;
using CqrsFramework.Events;
using MediatR;
using System.Threading.Tasks;
using System.Linq;

namespace Appointment.API.CommandHandlers
{
    public class CommandHandler
    {
        private readonly ISession _session;
        private readonly IEventPublisher _bus;
        //private readonly NotificationHandler _notifications;

        public CommandHandler(ISession session, IEventPublisher bus)
            :this(session)
        {
            _bus = bus;
        }

        public CommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task AddToSession(AggregateRoot aggregate)
        {
            await _session.Add(aggregate);
        }

        public async Task CommitSession()
        {
            try
            {
                await _session.Commit();

                //return true;
            }
            catch
            {
                
            }
        }
    }
}
