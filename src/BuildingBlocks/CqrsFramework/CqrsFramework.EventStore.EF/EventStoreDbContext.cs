using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CqrsFramework.EventSourcing
{
    public class EventStoreDbContext : DbContext
    {
        public const string SchemaName = "Events";

        public DbSet<Event> Events { get; set; }

        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>(ConfigureEventEntry);

        }

        void ConfigureEventEntry(EntityTypeBuilder<Event> builder)
        {
            
            builder.HasKey(x => x.Id /*new { x.AggregateId, x.AggregateType, x.Version }*/);
            builder.ToTable("Events");

            builder.Property(e => e.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();
            
            builder.Property(e => e.AggregateId)
                   .IsRequired();

            builder.Property(e => e.AggregateType);

            builder.Property(e => e.Version)
                .IsRequired();

            builder.Property(e => e.Payload)
                .IsRequired();

            builder.Property(e => e.CorrelationId);

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.EventType)
                .IsRequired();

        }
    }

    public class Event
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public int Version { get; set; }
        public string Payload { get; set; }
        public string CorrelationId { get; set; }
        public EventStateEnum State { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        // TODO: Following could be very useful for when rebuilding the read model from the event store, 
        // to avoid replaying every possible event in the system
        public string EventType { get; set; }
    }
}
