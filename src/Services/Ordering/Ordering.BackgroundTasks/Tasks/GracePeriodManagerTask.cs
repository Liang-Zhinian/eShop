﻿using Dapper;
using Eva.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.BackgroundTasks.Configuration;
using Ordering.BackgroundTasks.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ordering.BackgroundTasks.Tasks
{
    public class GracePeriodManagerService
         : BackgroundService
    {
        private readonly ILogger<GracePeriodManagerService> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IEventBus _eventBus;

        public GracePeriodManagerService(
            IOptions<BackgroundTaskSettings> settings,
            IEventBus eventBus,
            ILogger<GracePeriodManagerService> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("GracePeriodManagerService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#1 GracePeriodManagerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("GracePeriodManagerService background task is doing background work.");

                CheckConfirmedGracePeriodOrders();

                await Task.Delay(_settings.CheckUpdateTime, stoppingToken);
            }

            _logger.LogDebug("GracePeriodManagerService background task is stopping.");

            await Task.CompletedTask;
        }

        private void CheckConfirmedGracePeriodOrders()
        {
            _logger.LogDebug("Checking confirmed grace period orders");

            var orderIds = GetConfirmedGracePeriodOrders();

            foreach (var orderId in orderIds)
            {
                var confirmGracePeriodEvent = new GracePeriodConfirmedIntegrationEvent(orderId);

                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", confirmGracePeriodEvent.Id, Program.AppName, confirmGracePeriodEvent);

                _eventBus.Publish(confirmGracePeriodEvent);
            }
        }

        private IEnumerable<int> GetConfirmedGracePeriodOrders()
        {
            IEnumerable<int> orderIds = new List<int>();

            //using (var conn = new SqlConnection(_settings.ConnectionString))
            using (var conn = new MySqlConnection(_settings.ConnectionString))
            {
                try
                {
                    conn.Open();
                    orderIds = conn.Query<int>(
                        //@"SELECT Id FROM [ordering].[orders] 
                            //WHERE DATEDIFF(minute, [OrderDate], GETDATE()) >= @GracePeriodTime
                            //AND [OrderStatusId] = 1",
                        @"SELECT Id FROM `orders` 
                            WHERE DATE_ADD(`OrderDate`, INTERVAL @GracePeriodTime MINUTE) <= now()
                            AND `OrderStatusId` = 1",
                        new { GracePeriodTime = _settings.GracePeriodTime });
                }
                catch (MySqlException exception)
                {
                    _logger.LogCritical(exception, "FATAL ERROR: Database connections could not be opened: {Message}", exception.Message);
                }

            }

            return orderIds;
        }
    }
}
