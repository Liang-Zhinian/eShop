using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Locations
{
    public class AdditionalLocationImageCreatedEvent : IntegrationEvent
    {
        public AdditionalLocationImageCreatedEvent(Guid siteId, Guid locationId, string fileName)
        {
            SiteId = siteId;
            LocationId = locationId;
            FileName = fileName;
        }

        public Guid LocationId { get; set; }
        public Guid SiteId { get; set; }
        public string FileName { get; set; }
    }
}
