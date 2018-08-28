using System;
using CqrsFramework.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Sites
{
    public class LocationAssignedToSiteEvent : BaseEvent, IEvent
    {
        public Guid SiteId { get; set; }
        public Guid LocationId { get; set; }

        public LocationAssignedToSiteEvent()
        {
        }

        public LocationAssignedToSiteEvent(Guid id, Guid siteId, Guid locationId)
        {
            this.Id = id;
            this.SiteId = siteId;
            this.LocationId = locationId;
        }
    }
}
