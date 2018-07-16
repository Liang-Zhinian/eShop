using CqrsFramework.Domain.Exception;
using CqrsFramework.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Loader;
using System.Reflection;

namespace CqrsFramework.EventStore.SqlServerDB
{
    public class SqlServerEventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;
        private readonly string _connectionString;

        public SqlServerEventStore(IEventPublisher publisher, string connectionString)
        {
            _publisher = publisher;
            _connectionString = connectionString;
        }

        private void StoreEvents(object streamId, IEnumerable<object> events, long expectedInitialVersion)
        {
            events = events.ToArray();

            var connectionString = _connectionString;
            var serializedEvents = events.Select(x => new Tuple<string, string>(string.Format("{0}, {1}", x.GetType().FullName, x.GetType().GetTypeInfo().Assembly.GetName().Name), JsonConvert.SerializeObject(x)));

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                const string commandText = "Select Top 1 CurrentSequence from Streams where StreamId = @StreamId;";
                long? existingSequence;
                using (var command = new SqlCommand(commandText, con))
                {
                    command.Parameters.AddWithValue("StreamId", streamId.ToString());
                    var current = command.ExecuteScalar();
                    existingSequence = current == null ? (long?)null : (long)current;

                    if (existingSequence != null && ((long)existingSequence) > expectedInitialVersion)
                        throw new ConcurrencyException((Guid)streamId);
                }

                using (var t = con.BeginTransaction())
                {
                    try
                    {

                        var nextVersion = insertEventsAndReturnLastVersion(streamId, con, t, expectedInitialVersion, serializedEvents);

                        if (existingSequence == null)
                            startNewSequence(streamId, nextVersion, con, t);
                        else
                            updateSequence(streamId, expectedInitialVersion, nextVersion, con, t);

                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
            }
            foreach (var @event in events)
            {
                _publisher.Publish<IEvent>((IEvent)@event);
            }

        }

        static void updateSequence(object streamId, long expectedInitialVersion, long nextVersion, SqlConnection con, SqlTransaction trans)
        {
            const string cmdText =
                "Update Streams SET CurrentSequence = @CurrentSequence WHERE StreamID = @StreamID AND CurrentSequence = @OriginalSequence;";
            using (var cmd = new SqlCommand(cmdText, con))
            {
                cmd.Transaction = trans;
                cmd.Parameters.AddWithValue("StreamID", streamId.ToString());
                cmd.Parameters.AddWithValue("CurrentSequence", nextVersion);
                cmd.Parameters.AddWithValue("OriginalSequence", expectedInitialVersion);

                var rows = cmd.ExecuteNonQuery();
                if (rows != 1)
                    throw new ConcurrencyException((Guid)streamId);
            }
        }

        static void startNewSequence(object streamId, long nextVersion, SqlConnection con, SqlTransaction trans)
        {
            const string cmdText = "Insert into Streams (StreamId, CurrentSequence) values (@StreamId, @CurrentSequence);";
            using (var cmd = new SqlCommand(cmdText, con))
            {
                cmd.Transaction = trans;
                cmd.Parameters.AddWithValue("StreamId", streamId.ToString());
                cmd.Parameters.AddWithValue("CurrentSequence", nextVersion);

                int rows = cmd.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new ConcurrencyException((Guid)streamId);
                }
            }
        }

        static long insertEventsAndReturnLastVersion(object streamId, SqlConnection con, SqlTransaction trans, long nextVersion, IEnumerable<Tuple<string, string>> serializedEvents)
        {
            foreach (var e in serializedEvents)
            {
                const string insertText =
                    "Insert into EventWrappers (EventId, StreamId, Sequence, TimeStamp, EventType, Body) values (@EventId, @StreamId, @Sequence, @TimeStamp, @EventType, @Body);";
                using (var command = new SqlCommand(insertText, con))
                {
                    command.Transaction = trans;
                    command.Parameters.AddWithValue("EventId", Guid.NewGuid());
                    command.Parameters.AddWithValue("StreamId", streamId.ToString());
                    command.Parameters.AddWithValue("Sequence", nextVersion++);
                    command.Parameters.AddWithValue("TimeStamp", DateTime.UtcNow);
                    command.Parameters.AddWithValue("EventType", e.Item1);
                    command.Parameters.AddWithValue("Body", e.Item2);

                    command.ExecuteNonQuery();
                }
            }

            return nextVersion;
        }

        private IEnumerable<object> LoadEvents(Guid id, long version = 0)
        {
            const string cmdText = "SELECT EventType, BODY from EventWrappers WHERE StreamId = @StreamId AND Sequence >= @Sequence ORDER BY TimeStamp";
            var connectionString = _connectionString;
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(cmdText, con))
            {
                cmd.Parameters.AddWithValue("StreamId", id.ToString());
                cmd.Parameters.AddWithValue("Sequence", version);

                cmd.Connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventTypeString = reader["EventType"].ToString();
                    var eventType = Type.GetType(eventTypeString);
                    var serializedBody = reader["Body"].ToString();
                    yield return JsonConvert.DeserializeObject(serializedBody, eventType);
                }
            }
        }

        public IEnumerable<object> GetAllEventsEver()
        {
            const string cmdText = "SELECT EventType, BODY from EventWrappers ORDER BY TimeStamp";
            var connectionString = _connectionString;
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(cmdText, con))
            {
                cmd.Connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventTypeString = reader["EventType"].ToString();
                    var eventType = Type.GetType(eventTypeString);
                    var serializedBody = reader["Body"].ToString();
                    yield return JsonConvert.DeserializeObject(serializedBody, eventType);
                }
            }
        }

        public void Save(IEvent @event)
        {
            IEnumerable<object> events = new IEvent[] { @event };
            Guid streamId = @event.Id;
            long expectedInitialVersion = @event.Version;
            StoreEvents(streamId, events, expectedInitialVersion);
        }

        public IEnumerable<IEvent> Get(Guid aggregateId, int fromVersion)
        {
            IList<IEvent> events = new List<IEvent>();
            const string cmdText = "SELECT EventType, BODY from EventWrappers WHERE StreamId = @StreamId AND Sequence >= @Sequence ORDER BY TimeStamp";
            var connectionString = _connectionString;
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(cmdText, con))
            {
                cmd.Parameters.AddWithValue("StreamId", aggregateId.ToString());
                cmd.Parameters.AddWithValue("Sequence", fromVersion);

                cmd.Connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventTypeString = reader["EventType"].ToString();
                    var eventType = Type.GetType(eventTypeString);
                    var serializedBody = reader["Body"].ToString();
                    var @event = JsonConvert.DeserializeObject(serializedBody, eventType);
                    events.Add((IEvent)@event);
                }
            }

            return events;
        }
    }
}
