﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CqrsFramework.Commands;
using CqrsFramework.Events;
using CqrsFramework.Messages;

namespace CqrsFramework.Routing
{
    /// <summary>
    /// Default router implementation for sending commands and publishing events.
    /// </summary>
    public class Router : ICommandSender, IEventPublisher, IHandlerRegistrar
    {
        private readonly Dictionary<Type, List<Func<IMessage, CancellationToken, Task>>> _routes = new Dictionary<Type, List<Func<IMessage, CancellationToken, Task>>>();

        public void RegisterHandler<T>(Func<T, CancellationToken, Task> handler) where T : class, IMessage
        {
            if (!_routes.TryGetValue(typeof(T), out var handlers))
            {
                handlers = new List<Func<IMessage, CancellationToken, Task>>();
                _routes.Add(typeof(T), handlers);
            }
            handlers.Add((message, token) => handler((T)message, token));
        }

        public Task Send<T>(T command, CancellationToken cancellationToken = default(CancellationToken)) where T : class, ICommand
        {
            var type = command.GetType();
            if (!_routes.TryGetValue(type, out var handlers))
                throw new InvalidOperationException(string.Format("No handler registered for {0}", type.FullName));
            if (handlers.Count != 1)
                throw new InvalidOperationException(string.Format("Cannot send to more than one handler of {0}", type.FullName));
            return handlers[0](command, cancellationToken);
        }

        public Task Publish<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IEvent
        {
            if (!_routes.TryGetValue(@event.GetType(), out var handlers))
                return Task.FromResult(0);

            var tasks = new Task[handlers.Count];
            for (var index = 0; index < handlers.Count; index++)
            {
                tasks[index] = handlers[index](@event, cancellationToken);
            }
            return Task.WhenAll(tasks);
        }
    }
}
