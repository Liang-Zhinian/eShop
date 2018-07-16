using RabbitMQ.Client;
using System;

namespace SaaSEqt.eShop.BuildingBlocks.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
