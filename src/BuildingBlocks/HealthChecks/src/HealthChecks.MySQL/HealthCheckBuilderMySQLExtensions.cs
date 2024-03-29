﻿using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.HealthChecks;
using MySql.Data.MySqlClient;

namespace Eva.BuildingBlocks.HealthChecks.MySQL
{
    public static class HealthCheckBuilderMySQLExtensions
    {
        public static HealthCheckBuilder AddMySqlCheck(this HealthCheckBuilder builder, string name, string connectionString)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddMySqlCheck(builder, name, connectionString, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddMySqlCheck(this HealthCheckBuilder builder, string name, string connectionString, TimeSpan cacheDuration)
        {
            builder.AddCheck($"MySqlCheck({name})", async () =>
            {
                try
                {
                    //TODO: There is probably a much better way to do this.
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var cmd = new MySqlCommand("SHOW STATUS", connection))
                        {
                            var result = await cmd.ExecuteReaderAsync();
                            if (result.HasRows)
                            {
                                return HealthCheckResult.Healthy($"MySqlCheck({name}): Healthy");
                            }
                            return HealthCheckResult.Unhealthy($"MySqlCheck({name}): Unhealthy");
                        }

                        return HealthCheckResult.Unhealthy($"MySqlCheck({name}): Unhealthy");
                    }
                }
                catch (Exception ex)
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data["Exception"] = ex;
                    return HealthCheckResult.Unhealthy($"MySqlCheck({name}): Exception during check: {ex.GetType().FullName}", data);
                }
            }, cacheDuration);

            return builder;
        }
    }
}
