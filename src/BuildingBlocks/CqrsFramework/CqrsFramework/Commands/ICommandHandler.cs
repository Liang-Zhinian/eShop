﻿using CqrsFramework.Messages;

namespace CqrsFramework.Commands
{
    /// <summary>
    /// Defines a handler for a command.
    /// </summary>
    /// <typeparam name="T">Event type being handled</typeparam>
    public interface ICommandHandler<in T> : IHandler<T> where T : ICommand
    {
    }
}