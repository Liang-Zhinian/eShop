using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

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
                     string userName,
                     string password,
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
            this.UserName = userName;
            this.Password = password;
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

        public string UserName { get; private set; }

        public string Password { get; private set; }

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

        public bool IsMale { get; private set; } = true;

        public string Bio { get; private set; }

        public string Image { get; private set; }

        [NotMapped]
        public string ImageUri { get; set; }

        public bool CanLoginAllLocations { get; private set; } = false;

        public Guid SiteId { get; private set; }
        public virtual Site Site { get; private set; }

        public Guid TenantId { get; private set; }

        public bool IsEnabled { get; private set; } = true;

        public virtual ICollection<StaffLoginLocation> StaffLoginLocations { get; private set; }


        #region setter methods

        public void UpdateImage(string image){
            this.Image = image;
        }

        public void Activate(){
            this.IsEnabled = true;
        }

        public void Deactivate()
        {
            this.IsEnabled = false;
        }

        public void UpdateBriefInfo(string firstName, 
                                    string lastName,
                                    string bio,
                                    bool isMale, 
                                    string emailAddress, 
                                    string primaryTelephone, 
                                    string secondaryTelephone){
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Bio = bio;
            this.IsMale = isMale;
            this.EmailAddress = emailAddress;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
        }

        public void UpdateAddress(
                     string addressStreetAddress,
                     string addressCity,
                     string addressStateProvince,
                     string addressPostalCode,
                     string addressCountryCode)
        {
            this.StreetAddress = addressStreetAddress;
            this.City = addressCity;
            this.StateProvince = addressStateProvince;
            this.PostalCode = addressPostalCode;
            this.CountryCode = addressCountryCode;
        }

        #endregion
    }
}
