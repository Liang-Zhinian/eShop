using System;
using System.Collections.Generic;

namespace SaaSEqt.eShop.Services.Business.Model
{
    public class Branding
    {
        private Branding()
        {

        }

        public Branding(Guid siteId, byte[] logo, string pageColor1, string pageColor2, string pageColor3, string pageColor4)
        {
            this.SiteId = siteId;
            //this.Site = site;
            this.Id = Guid.NewGuid();
            this.Logo = logo;
            this.PageColor1 = pageColor1;
            this.PageColor2 = pageColor2;
            this.PageColor3 = pageColor3;
            this.PageColor4 = pageColor4;
        }

        /// A site Id unique to a business
        public Guid Id { get; private set; }
        /// The image data to the site logo
        public byte[] Logo { get; private set; }
        /// Page color
        public string PageColor1 { get; private set; }
        /// Page color
        public string PageColor2 { get; private set; }
        /// Page color
        public string PageColor3 { get; private set; }
        /// Page color
        public string PageColor4 { get; private set; }

        public Guid SiteId { get; private set; }
        public virtual Site Site { get; private set; }

        //public static Branding CreateDefaultBranding(TenantId tenantId, Site site) {
        //    Branding branding = new Branding(tenantId, site, "logo", "color1", "color2", "color3", "color4");

        //    return branding;
        //}
    }
}
