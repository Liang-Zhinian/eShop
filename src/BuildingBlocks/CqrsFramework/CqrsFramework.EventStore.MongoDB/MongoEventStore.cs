using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CqrsFramework.Domain.Exception;
using CqrsFramework.Events;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CqrsFramework.EventStore.MongoDB
{
    public class Streams
    {
        public ObjectId _id { get; set; }
        public Guid StreamID { get; set; }
        public long CurrentSequence { get; set; }
    }

    public class EventWrappers
    {
        public ObjectId _id { get; set; }
        public Guid EventId { get; set; }
        public Guid StreamId { get; set; }
        public long Sequence { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EventType { get; set; }
        public string Body { get; set; }
    }

    public class MongoEventStore : IEventStore
    {
        private MongoClient client;
        private MongoCollectionSettings _commitSettings;

        protected const string DEFAULT_DATABASE_URI = "mongodb://127.0.0.1:27017";

        public MongoEventStore(string connectionString = DEFAULT_DATABASE_URI)
            :this(new MongoClient(connectionString))
        {
            
        }

        public MongoEventStore(MongoClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            this.client = client;
            BsonClassMap.RegisterClassMap<EventWrappers>();
            BsonClassMap.RegisterClassMap<Streams>();
            _commitSettings = new MongoCollectionSettings { AssignIdOnInsert = false, WriteConcern = WriteConcern.Acknowledged };


            //BsonClassMap.RegisterClassMap<IEvent>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var domainEventSubclasses = new List<Type>();
            foreach (var a in assemblies)
            {
                var types = a.GetTypes().Where(t => typeof(EventWrappers).IsAssignableFrom(t) || typeof(Streams).IsAssignableFrom(t));
                domainEventSubclasses.AddRange(types);
            }

            foreach (var subclass in domainEventSubclasses)
            {
                BsonClassMap.LookupClassMap(subclass);
            }
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            // SELECT EventType, BODY from EventWrappers WHERE StreamId = @StreamId AND Sequence >= @Sequence ORDER BY TimeStamp
            var db = this.client.GetDatabase("EventStore");
            var query = Builders<EventWrappers>.Filter.And(new FilterDefinition<EventWrappers>[]{
                Builders<EventWrappers>.Filter.Eq(s => s.StreamId, aggregateId),
                Builders<EventWrappers>.Filter.Gte(s => s.Sequence, fromVersion)
            });

            var events = db.GetCollection<EventWrappers>("EventWrappers", _commitSettings);
            var docs = events.Find<EventWrappers>(query);
            if (docs == null) return null;

            IList<IEvent> list = new List<IEvent>();

            foreach (var doc in docs.ToList())
            {
                var eventTypeString = doc.EventType.ToString();
                var eventType = Type.GetType(eventTypeString);
                var serializedBody = doc.Body.ToString();
                var @event = JsonConvert.DeserializeObject(serializedBody, eventType);
                list.Add((IEvent)@event);
            }

            return Task.FromResult(list.AsEnumerable());

        }

        public Task Save(IEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<object> events = new IEvent[] { @event };
            Guid streamId = @event.Id;
            long expectedInitialVersion = @event.Version;
            SaveEvents(streamId, events, expectedInitialVersion);
            return Task.CompletedTask;
        }

        public void SaveEvents(object streamId, IEnumerable<object> events, long expectedInitialVersion)
        {
            events = events.ToArray();

            var serializedEvents = events.Select(x => new Tuple<string, string>(string.Format("{0}, {1}", x.GetType().FullName, x.GetType().GetTypeInfo().Assembly.GetName().Name), JsonConvert.SerializeObject(x)));

            // get CurrentSequence from Streams by StreamId
                long? existingSequence;

            var db = this.client.GetDatabase("EventStore");
            var query = Builders<Streams>.Filter.Eq(s => s.StreamID, streamId);
            var streams = db.GetCollection<Streams>("Streams", _commitSettings);
            var stream = streams.Find<Streams>(query).FirstOrDefault();

            existingSequence = stream == null ? (long?)null : (long)stream.CurrentSequence;

                    if (existingSequence != null && ((long)existingSequence) > expectedInitialVersion)
                        throw new ConcurrencyException((Guid)streamId);


            // to do: using mongodb transaction

            try
            {

                var nextVersion = insertEventsAndReturnLastVersion(streamId, expectedInitialVersion, serializedEvents);

                if (existingSequence == null)
                    startNewSequence(streamId, nextVersion);
                else
                    updateSequence(streamId, expectedInitialVersion, nextVersion);

                // to do: commit mongodb transaction
            }
            catch (Exception e)
            {
                // to do: rollback mongodb transaction
                throw e;
            }
        }

        public void updateSequence(object streamId, long expectedInitialVersion, long nextVersion)
        {
            // update Streams
            var db = this.client.GetDatabase("EventStore");
            var streams = db.GetCollection<Streams>("Streams", _commitSettings);
            var query = Builders<Streams>.Filter.And(new FilterDefinition<Streams>[]{
                Builders<Streams>.Filter.Eq(s => s.StreamID, streamId),
                Builders<Streams>.Filter.Eq(s => s.CurrentSequence, expectedInitialVersion)
            });

            var update = Builders<Streams>.Update.Set("CurrentSequence", nextVersion);
            var options = new FindOneAndUpdateOptions<Streams>();
            Streams result = streams.FindOneAndUpdate(query, update, options);

            if (result == null)
                throw new ConcurrencyException((Guid)streamId);
        }

        public void startNewSequence(object streamId, long nextVersion)
        {
            // Insert into Streams
            var db = this.client.GetDatabase("EventStore");
            var streams = db.GetCollection<Streams>("Streams", _commitSettings);

            streams.InsertOne(new Streams
            {
                StreamID = (System.Guid)streamId,
                CurrentSequence = nextVersion
            });
        }

        public long insertEventsAndReturnLastVersion(object streamId, long nextVersion, IEnumerable<Tuple<string, string>> serializedEvents)
        {
            foreach (var e in serializedEvents)
            {
                // insert into EventWrappers
                var db = this.client.GetDatabase("EventStore");
                var events = db.GetCollection<EventWrappers>("EventWrappers", _commitSettings);


                events.InsertOne(new EventWrappers
                {
                    EventId = Guid.NewGuid(),
                    StreamId = (System.Guid)streamId,
                    Sequence = nextVersion++,
                    TimeStamp = DateTime.UtcNow,
                    EventType = e.Item1,
                    Body = e.Item2
                });
            }

            return nextVersion;
        }


        public IEnumerable<object> GetAllEventsEver()
        {
            var db = this.client.GetDatabase("EventStore");
            var eventsCollection = db.GetCollection<EventWrappers>("EventWrappers", _commitSettings);

            var events = db.GetCollection<EventWrappers>("EventWrappers", _commitSettings);
            var sortBy = Builders<EventWrappers>.Sort.Ascending(_ => _.TimeStamp);
            var docs = events.Find<EventWrappers>(_=>true).Sort(sortBy);
            if (docs == null) yield return null;

            //List<EventWrappers> list = docs.ToList();
            long count = docs.Count();
  
            foreach (var doc in docs.ToList())
            {
                var eventTypeString = doc.EventType.ToString();
                var eventType = Type.GetType(eventTypeString);
                var serializedBody = doc.Body.ToString();
                yield return JsonConvert.DeserializeObject(serializedBody, eventType);
            }

        }

        public Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
