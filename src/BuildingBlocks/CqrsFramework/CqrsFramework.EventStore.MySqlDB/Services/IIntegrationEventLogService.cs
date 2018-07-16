using System.Data.Common;
using System.Threading.Tasks;
using CqrsFramework.Events;

namespace CqrsFramework.EventStore.MySqlDB.Services
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IEvent @event, DbTransaction transaction);
        Task MarkEventAsPublishedAsync(IEvent @event);
    }
}
