using System;
using CqrsFramework.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Sites
{
    public class SiteBrandingAppliedEvent : IEvent
    {
        public Guid Id { get; set; }

        public Guid BrandingId {get;set;}

        /// The image data to the site logo
        public byte[] Logo { get; set; }
        /// Page color
        public string PageColor1 { get; set; }
        /// Page color
        public string PageColor2 { get; set; }
        /// Page color
        public string PageColor3 { get; set; }
        /// Page color
        public string PageColor4 { get; set; }

        public Guid SiteId { get; set; }

        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public SiteBrandingAppliedEvent()
        {
        }

        public SiteBrandingAppliedEvent(Guid sourceId, Guid siteId, Guid brandingId, byte[] logo, string pageColor1, string pageColor2, string pageColor3, string pageColor4)
        {
            this.Id = sourceId;
            this.SiteId = siteId;
            this.BrandingId = brandingId;
            this.Logo = logo;
            this.PageColor1 = pageColor1;
            this.PageColor2 = pageColor2;
            this.PageColor3 = pageColor3;
            this.PageColor4 = pageColor4;
            this.Version = 1;
            this.TimeStamp = DateTimeOffset.Now;
        }
    }
}
