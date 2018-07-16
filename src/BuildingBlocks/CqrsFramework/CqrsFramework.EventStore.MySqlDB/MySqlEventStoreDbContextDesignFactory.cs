using CqrsFramework.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CqrsFramework.EventStore.MySqlDB
{
    public class MySqlEventStoreDbContextDesignFactory : IDesignTimeDbContextFactory<EventStoreDbContext>
    {
        public EventStoreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventStoreDbContext>();
            optionsBuilder.UseMySql("Server=localhost;database=book2business;uid=root;pwd=P@ssword;charset=utf8;port=3306;SslMode=None", 
                                    b => b.MigrationsAssembly("CqrsFramework.EventStore.MySqlDB"));

            return new EventStoreDbContext(optionsBuilder.Options);
        }
    }
}
