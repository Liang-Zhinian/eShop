using System;
using SaaSEqt.eShop.BuildingBlocks.EventBus.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Sites
{
    public class SiteCreatedEvent : IntegrationEvent
    {
        public Guid SiteId { get; set; }

        //public int Version { get; set; }

        //public DateTimeOffset TimeStamp { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public string ContactName { get; set; }

        public string PrimaryTelephone { get; set; }

        public string SecondaryTelephone { get; set; }

        public Guid TenantId { get; set; }

        public SiteCreatedEvent(Guid tenantId, Guid siteId, string name, string description, bool active, string contactName, string primaryTelephone, string secondaryTelephone)
        {
            this.SiteId = siteId;
            //this.Version = 1;
            //this.TimeStamp = DateTimeOffset.Now;
            this.Name = name;
            this.Description = description;
            this.Active = active;
            this.ContactName = contactName;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
            this.TenantId = tenantId;
        }
    }
}
