using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using CqrsFramework.Commands;
using CqrsFramework.Events;
using CqrsFramework.Messages;
using CqrsFramework.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
namespace CqrsFramework.Bus.RabbitMQ
{
    public class RabbitMQBus : IDisposable, IHandlerRegistrar, ICommandSender, IEventPublisher/*, IMessageReceiver*/
    {
        //private readonly IConnectionFactory _connectionFactory;
        //private readonly IConnection _connection;
        private readonly string _exchangeName;
        private readonly string _exchangeType;
        private readonly bool _autoAck;
        private bool _disposed;
        private bool _disposing = false;

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMQBus> _logger;
        private readonly ILifetimeScope _autofac;
        private readonly int _retryCount;
        private readonly string AUTOFAC_SCOPE_NAME = "saaseqt_event_bus";

        private IModel _consumerChannel;
        private string _queueName;

        private readonly Dictionary<Type, List<Func<IMessage, CancellationToken, Task>>> _routes = new Dictionary<Type, List<Func<IMessage, CancellationToken, Task>>>();
        private readonly List<Type> _eventTypes = new List<Type>();
        //public event EventHandler<MessageReceivedEventArgs> MessageReceived;


        public RabbitMQBus(IRabbitMQPersistentConnection persistentConnection, 
                           ILogger<RabbitMQBus> logger,
                           ILifetimeScope autofac, 
                            string exchangeName,
                            string exchangeType = ExchangeType.Fanout,
                           string queueName = null,
                            bool autoAck = false,
                           int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
            this._exchangeType = exchangeType;
            this._exchangeName = exchangeName;
            _queueName = queueName;
            this._autoAck = autoAck;
            _autofac = autofac;
            _consumerChannel = CreateConsumerChannel();
        }

        #region public methods

        public void Dispose()
        {
            this.Stop();
            GC.SuppressFinalize(this);
        }

        public void RegisterHandler<T>(Func<T, CancellationToken, Task> handler) where T : class, IMessage
        {
            if (!_routes.TryGetValue(typeof(T), out var handlers))
            {
                handlers = new List<Func<IMessage, CancellationToken, Task>>();
                _routes.Add(typeof(T), handlers);
            }
            handlers.Add((message, token) => handler((T)message, token));
            _eventTypes.Add(typeof(T));
            DoInternalSubscription(typeof(T).Name);
        }

        public Task Send<T>(T command, CancellationToken cancellationToken = default(CancellationToken)) where T : class, ICommand
        {
            string message = JsonConvert.SerializeObject(command/*, 
                                                         new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }*/);
            //byte[] body = Encoding.UTF8.GetBytes(message);

            SendMessage(message, command.GetType().Name);
            return Task.CompletedTask;
        }

        public Task Publish<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IEvent
        {
            string message = JsonConvert.SerializeObject(@event/*, 
                                                         new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }*/);
            //var body = Encoding.UTF8.GetBytes(message);

            SendMessage(message, @event.GetType().Name);
            return Task.CompletedTask;
        }

        public void Start()
        {
            //this._queueName = this.ReceiveMessages(this._queueName);
            //_consumerChannel = CreateConsumerChannel();
        }

        public void Stop()
        {
            if (!_disposed)
            {
                if (_disposing)
                {
                    this._consumerChannel.Dispose();
                    //this._connection.Dispose();

                    _logger.LogInformation($"RabbitMQBus has been disposed. Hash Code:{this.GetHashCode()}.");
                }

                _disposed = true;
                _disposing = false;
            }
        }

        #endregion

        #region private methods

        private void SendMessage(string message, string routingKey = "")
        {
            var body = Encoding.UTF8.GetBytes(message);

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex.ToString());
                });

            using (var channel = _persistentConnection.CreateModel())
            {
                
                channel.ExchangeDeclare(this._exchangeName, this._exchangeType, true, false);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    channel.BasicPublish(exchange: _exchangeName,
                                         routingKey: routingKey,
                                     mandatory: true,
                                     basicProperties: properties,
                                     body: body);
                });
            }
        }

        private IModel CreateConsumerChannel()
        {
            Task.Delay(3000);

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(this._exchangeName, this._exchangeType, true, false);

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);

                if (!_autoAck)
                {
                    channel.BasicAck(ea.DeliveryTag, multiple: false);
                    Console.WriteLine(" Ack sent: {0}", message);
                    _logger.LogInformation($"Ack sent: \n" + message);
                }

            };

            channel.BasicConsume(queue: _queueName,
                                 autoAck: this._autoAck,
                                 consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            _logger.LogInformation($"RabbitMQBus has created the consumer chanel. Hash Code:{this.GetHashCode()}.");
            return channel;
        }

        private async Task ProcessEvent(string eventName, string message, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    Console.WriteLine(" [x] {0}", message);
                    _logger.LogInformation($"RabbitMQBus is processing an event: \n" + message);

                    //this.MessageReceived(this, new MessageReceivedEventArgs(message));
                    try
                    {
                        var eventType = GetEventTypeByName(eventName);
                        var eventData = JsonConvert.DeserializeObject(
                            message,
                            eventType
                        /*new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }*/
                        );
                        var @event = (IEvent)eventData;

                        if (_routes.TryGetValue(@event.GetType(), out var handlers))
                        {

                            //var tasks = new Task[handlers.Count];
                            for (var index = 0; index < handlers.Count; index++)
                            {
                                await handlers[index](@event, cancellationToken);
                            }

                            //if (!_autoAck)
                            //{
                            //    _consumerChannel.BasicAck(ea.DeliveryTag, false);
                            //    Console.WriteLine(" Ack sent: {0}", message);
                            //    _logger.LogInformation($"Ack sent: \n" + message);
                            //}
                            //return Task.WhenAll(tasks);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                        //return Task.FromResult(0);
                        //throw e;
                    }
                }
            }
        }

        private bool HasSubscriptionsForEvent(string eventName) => _routes.Any(_ => _.Key.Name.Equals(eventName));

        private Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        private string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        private void DoInternalSubscription(string eventName)
        {

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(queue: _queueName,
                                  exchange: this._exchangeName,
                                  routingKey: eventName);
            }

        }

        #endregion
    }
}
