using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaaSEqt.eShop.Business.Domain.Model.Security
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

        public Guid Id { get; private set; }

        //public string UserName { get; private set; }

        public bool IsMale { get; private set; }

        public string Bio { get; private set; }

        public string Image { get; private set; }

        public bool CanLoginAllLocations { get; private set; }

        public Guid SiteId { get; private set; }
        public virtual Site Site { get; private set; }

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
