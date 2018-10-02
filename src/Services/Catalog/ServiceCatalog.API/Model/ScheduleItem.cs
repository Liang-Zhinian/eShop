using System;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Model
{
    public abstract class ScheduleItem
    {
        protected ScheduleItem()
        {
        }

        protected ScheduleItem(Guid siteId, Guid staffId, Guid serviceItemId, Guid locationId, DateTime startTime, DateTime endTime
                               /*, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday*/)
        {
            this.Id = Guid.NewGuid();
            this.SiteId = siteId;
            this.StaffId = staffId;
            this.ServiceItemId = serviceItemId;
            this.LocationId = locationId;
            this.StartDateTime = startTime;
            this.EndDateTime = endTime;
            //this.Sunday = Sunday;
            //this.Monday = Monday;
            //this.Tuesday = Tuesday;
            //this.Wednesday = Wednesday;
            //this.Thursday = Thursday;
            //this.Friday = Friday;
            //this.Saturday = Saturday;
        }

        /// The unique ID
        public Guid Id { get; set; }

        /// Start time
        public DateTime StartDateTime { get; set; }

        /// End time.
        public DateTime EndDateTime { get; set; }

        ///// Staff, teacher, or trainer
        public Guid StaffId { get; set; }

        /// The session type of the schedule
        public Guid ServiceItemId { get; set; }
        public virtual ServiceItem ServiceItem { get; set; }

        /// The location of the schedule
        public Guid LocationId { get; set; }

        public Guid SiteId { get; set; }

        //public bool Sunday { get; private set; }
        //public bool Monday { get; private set; }
        //public bool Tuesday { get; private set; }
        //public bool Wednesday { get; private set; }
        //public bool Thursday { get; private set; }
        //public bool Friday { get; private set; }
        //public bool Saturday { get; private set; }
    }
}
