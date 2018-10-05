using System.Threading.Tasks;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Abstractions;
using SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.Events.Staffs;
using SaaSEqt.eShop.Services.Sites.API.Infrastructure;
using SaaSEqt.eShop.Services.Sites.API.Model;

namespace SaaSEqt.eShop.Services.Sites.API.Application.IntegrationEvents.EventHandling
{
	public class StaffCreatedEventHandler : IIntegrationEventHandler<StaffCreatedEvent>
    {
        private readonly SitesContext _siteDbContext;
        public StaffCreatedEventHandler(SitesContext siteDbContext)
        {
            _siteDbContext = siteDbContext;
        }

		public async Task Handle(StaffCreatedEvent @event)
        {
            Staff newStaff = new Staff(@event.Id, @event.SiteId, @event.TenantId, 
                                       @event.FirstName, @event.LastName, @event.Enabled, 
                                       @event.EmailAddress, @event.PrimaryTelephone, @event.SecondaryTelephone, 
                                       @event.AddressStreetAddress, @event.AddressCity, @event.AddressStateProvince, 
                                       @event.AddressPostalCode, @event.AddressCountryCode);

            await _siteDbContext.AddAsync(newStaff);
            await _siteDbContext.SaveChangesAsync();
        }
    }
}
