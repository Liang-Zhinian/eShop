using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaaSEqt.eShop.Services.Sites.API.Model
{
    public class Staff
    {

        private Staff() { }

        public Staff(Guid siteId, Guid staffId, bool isMale, string image, string bio, bool canLoginAllLocations)
        {
            Id = staffId;
            this.SiteId = siteId;
            this.IsMale = isMale;
            this.Image = image;
            this.Bio = bio;
            this.CanLoginAllLocations = canLoginAllLocations;
        }

        public Staff(Guid staffId,
                     Guid siteId,
                     Guid tenantId,
                     string firstName,
                     string lastName,
                     bool enabled,
                     string emailAddress,
                     string primaryTelephone,
                     string secondaryTelephone,
                     string addressStreetAddress,
                     string addressCity,
                     string addressStateProvince,
                     string addressPostalCode,
                     string addressCountryCode)
        {
            this.Id = staffId;
            this.SiteId = siteId;
            this.TenantId = tenantId;
            //this.Username = username;
            //this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IsEnabled = enabled;
            //this.StartDate = startDate;
            //this.EndDate = endDate;
            this.EmailAddress = emailAddress;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
            this.StreetAddress = addressStreetAddress;
            this.City = addressCity;
            this.StateProvince = addressStateProvince;
            this.PostalCode = addressPostalCode;
            this.CountryCode = addressCountryCode;
        }

        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string EmailAddress { get; private set; }

        public string City { get; private set; }

        public string CountryCode { get; private set; }

        public string PostalCode { get; private set; }

        public string StateProvince { get; private set; }

        public string StreetAddress { get; private set; }

        public string PrimaryTelephone { get; private set; }

        public string SecondaryTelephone { get; private set; }

        public bool IsMale { get; private set; }

        public string Bio { get; private set; }

        public string Image { get; private set; }

        public bool CanLoginAllLocations { get; private set; }

        public Guid SiteId { get; private set; }
        public virtual Site Site { get; private set; }

        public Guid TenantId { get; private set; }

        public bool IsEnabled { get; private set; }

        public virtual ICollection<StaffLoginLocation> StaffLoginLocations { get; private set; }
        //public virtual ICollection<Availability> Availibilities { get; private set; }
        //public virtual ICollection<Unavailability> Unavailabilities { get; private set; }

        //public Availability AddAvailability(Guid serviceItemId, Guid locationId, DateTime startTime, DateTime endTime, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, DateTime bookableEndTime) {
        //    Availability availability = new Availability(this.SiteId, this.Id, serviceItemId, locationId, startTime, endTime, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, bookableEndTime);

        //    if (Availibilities == null) Availibilities = new ObservableCollection<Availability>();

        //    Availibilities.Add(availability);

        //    return availability;
        //}

        //public Unavailability AddUnavailability(Guid serviceItemId, Guid locationId, DateTime startTime, DateTime endTime, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, string description)
        //{
        //    Unavailability unavailability = new Unavailability(this.SiteId, this.Id, serviceItemId, locationId, startTime, endTime, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, description);

        //    if (Unavailabilities == null) Unavailabilities = new ObservableCollection<Unavailability>();

        //    Unavailabilities.Add(unavailability);

        //    return unavailability;
        //}

    }
}
