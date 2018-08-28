using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaaSEqt.eShop.Business.Domain.Model.Security
{
    public class Site // : AggregateRoot
    {
        private Site()
        {
        }

        public Site(Guid tenantId,
                    string siteName,
                    string siteDescription,
                    bool active
                   )
        {
            this.TenantId = tenantId;
            this.Id = Guid.NewGuid();
            this.Name = siteName;
            this.Description = siteDescription;
            this.Active = active;
            this.ContactInformation = ContactInformation.Empty();
            this.Locations = new ObservableCollection<Location>();
            this.Staffs = new ObservableCollection<Staff>();

            //Locations = new ObservableCollection<Location>();

            //ApplyChange(new SiteCreatedEvent(this.Id, siteName, siteDescription, active, tenantId.Id, contactInformation.ContactName, contactInformation.PrimaryTelephone, contactInformation.SecondaryTelephone));
        }

        #region public properties

        public Guid Id { get; private set; }

        /// The name of the site
        public string Name { get; private set; }
        /// Site description
        public string Description { get; private set; }

        public bool Active { get; private set; }

        //public Guid? BrandingId { get; private set; }
        public Branding Branding { get; private set; }

        public ContactInformation ContactInformation { get; private set; }

        public Guid TenantId { get; private set; }

        public virtual ICollection<Location> Locations { get; private set; }
        public virtual ICollection<Staff> Staffs { get; private set; }

        #endregion

        #region [ Command Methods which Publish Domain Events ]

        public Location ProvisionLocation(Location location)
        {
            // To Do: send event
            if (Locations == null) Locations = new ObservableCollection<Location>();
            Locations.Add(location);

            //ApplyChange(new LocationAssignedToSiteEvent(this.Id, this.Id, location.Id));

            return location;
        }

        public void UpdateBranding(string logo, string pageColor1, string pageColor2, string pageColor3, string pageColor4)
        {
            if (null == this.Branding)
                this.Branding = new Branding(this.Id, logo, pageColor1, pageColor2, pageColor3, pageColor4);
            else
                this.Branding.UpdateBranding(logo, pageColor1, pageColor2, pageColor3, pageColor4);


            //ApplyChange(new SiteBrandingAppliedEvent(this.Id, this.Id, this.Branding.Id, logo, pageColor1, pageColor2, pageColor3, pageColor4));
        }

        public void UpdateContactInformation(string contactName, string primaryTelephone, string secondaryTelephone, string emailAddress)
        {
            this.ContactInformation = new ContactInformation(contactName, primaryTelephone, secondaryTelephone, emailAddress);

            // To Do: send event
        }

        public void Activate()
        {
            if (!this.Active)
            {
                this.Active = true;
            }
        }

        public void Deactivate()
        {
            if (this.Active)
            {
                this.Active = false;
            }
        }

        #endregion

        #region [ Apply event methods ]

        //public void Apply(SiteCreatedEvent @event) {
        //    this.Name = @event.Name;
        //    this.Description = @event.Description;
        //    this.Active = @event.Active;
        //    this.TenantId = new TenantId(@event.TenantId);
        //}

        //public void Apply(SiteBrandingAppliedEvent @event) {
        //    this.Branding = new Branding(this.Id, @event.Logo, @event.PageColor1, @event.PageColor2, @event.PageColor3, @event.PageColor4);
        //}

        #endregion
    }
}
