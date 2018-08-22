using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaaSEqt.eShop.Business.Domain.Model.Security
{
    public class Location // : AggregateRoot
    {
        #region ctors

        private Location(){}

        public Location(Guid siteId,
                        string name, 
                        string description,
                        bool active
                       )
        {
			this.Id = Guid.NewGuid();
            this.SiteId = siteId;
            this.Name = name;
            this.Description = description;
            this.Active = active;
            this.Image = null;
            this.ContactInformation = ContactInformation.Empty();
            this.Address = Address.Empty();
            this.Geolocation = Geolocation.Empty();

            this.AdditionalLocationImages = new ObservableCollection<LocationImage>();


            //ApplyChange(new LocationCreatedEvent(
                       //         this.Id,
                       //         siteId,
                       //         name,
                       //         description,
                       //         contactInformation.ContactName,
                       //         contactInformation.EmailAddress,
                       //         contactInformation.PrimaryTelephone,
                       //         contactInformation.SecondaryTelephone
                       //     )
                       //);
        }

        #endregion

        #region public properties

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Image { get; private set; }

        [NotMapped]
        public string ImageUri { get; set; }

        public ContactInformation ContactInformation { get; private set; }

        public Address Address { get; private set; }

        public Geolocation Geolocation { get; private set; }

        public bool Active { get; private set; }

        public Guid SiteId { get; private set; }
        public Site Site { get; private set; }

        public ICollection<LocationImage> AdditionalLocationImages { get; private set; }

        #endregion

        #region [Command Methods which Publish Domain Events]

        public void AddAdditionalImage(LocationImage image){
            if (AdditionalLocationImages == null)
                AdditionalLocationImages = new List<LocationImage>();
            
            this.AdditionalLocationImages.Add(image);

            //ApplyChange(new AdditionalLocationImageCreatedEvent(image.Id, image.SiteId, this.Id, image.Image));
        }

        public void ChangeAddress(Address address){
            this.Address.Street = address.Street;
            this.Address.City = address.City;
            this.Address.StateProvince = address.StateProvince;
            this.Address.ZipCode = address.ZipCode;
            this.Address.Country = address.Country;

            //ApplyChange(new LocationAddressChangedEvent(this.Id, this.SiteId, postalAddress.StreetAddress,
                                                        //postalAddress.StreetAddress2, postalAddress.City,
                                                        //postalAddress.StateProvince, postalAddress.PostalCode,
                                                        //postalAddress.CountryCode));
        }

        public void ChangeGeolocation(Geolocation geolocation)
        {
            this.Geolocation.Latitude = geolocation.Latitude;
            this.Geolocation.Longitude = geolocation.Longitude;
            //this.Latitude = latitude;
            //this.Longitude = longitude;

            //ApplyChange(new LocationGeolocationChangedEvent(this.Id, this.SiteId, geolocation.Latitude, geolocation.Longitude));
        }

        public void ChangeImage(string image)
        {
            this.Image = image;

            //ApplyChange(new LocationImageChangedEvent(this.Id, this.SiteId, image));
        }

        public void ChangeContactInformation(ContactInformation contactInformation){
            this.ContactInformation.ContactName = contactInformation.ContactName;
            this.ContactInformation.EmailAddress = contactInformation.EmailAddress;
            this.ContactInformation.PrimaryTelephone = contactInformation.PrimaryTelephone;
            this.ContactInformation.SecondaryTelephone = contactInformation.SecondaryTelephone;
        }

        //public void AssignResource(Resource resource)
        //{
        //    if (ResourceLocations == null)
        //        ResourceLocations = new List<ResourceLocation>();

        //    ResourceLocation resourceLocation = new ResourceLocation(resource, this);

        //    this.ResourceLocations.Add(resourceLocation);
        //}

        #endregion

        #region [ Apply event methods ]

        //public void Apply(LocationCreatedEvent message)
        //{
        //    this.Name = message.Name;
        //    this.Description = message.Description;
        //    this.ContactInformation = new ContactInformation(message.ContactName,
        //                                                     message.PrimaryTelephone,
        //                                                     message.SecondaryTelephone,
        //                                                     message.EmailAddress);
        //    this.SiteId = message.SiteId;

        //}

        //public void Apply(LocationAddressChangedEvent message)
        //{
        //    this.PostalAddress = new PostalAddress(message.StreetAddress,
        //                                            message.StreetAddress2,
        //                                            message.City,
        //                                            message.StateProvince,
        //                                            message.PostalCode,
        //                                           message.CountryCode);

        //    this.SiteId = message.SiteId;
        //}

        //public void Apply(LocationImageChangedEvent message)
        //{
        //    this.Image = message.Image;

        //    this.SiteId = message.SiteId;
        //}

        //public void Apply(LocationGeolocationChangedEvent message)
        //{
        //    this.Geolocation = new Geolocation(message.Latitude, message.Longitude);

        //    this.SiteId = message.SiteId;
        //}

        //public void Apply(AdditionalLocationImageCreatedEvent message)
        //{
        //    this.AdditionalLocationImages.Add(new LocationImage(this.SiteId, this.Id, message.Image));

        //    this.SiteId = message.SiteId;
        //}


        #endregion
    }
}
