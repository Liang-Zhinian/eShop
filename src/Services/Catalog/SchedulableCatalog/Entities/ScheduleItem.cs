using System;

namespace SaaSEqt.eShop.Services.SchedulableCatalog.Entities
{
    public abstract class ScheduleItem
    {
        protected ScheduleItem()
        {
        }

        protected ScheduleItem(Guid siteId, Guid staffId, Guid schedulableCatalogItemId, Guid locationId, DateTime startTime, DateTime endTime
                               /*, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday*/)
        {
            this.Id = Guid.NewGuid();
            this.SiteId = siteId;
            this.StaffId = staffId;
            this.SchedulableCatalogItemId = schedulableCatalogItemId;
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
        public Guid Id { get; private set; }

        /// Start time
        public DateTime StartDateTime { get; private set; }

        /// End time.
        public DateTime EndDateTime { get; private set; }

        ///// Staff, teacher, or trainer
        public Guid StaffId { get; private set; }

        /// The session type of the schedule
        public Guid SchedulableCatalogItemId { get; private set; }
        public virtual SchedulableCatalogItem SchedulableCatalogItem { get; private set; }

        /// The location of the schedule
        public Guid LocationId { get; private set; }

        public Guid SiteId { get; private set; }

        //public bool Sunday { get; private set; }
        //public bool Monday { get; private set; }
        //public bool Tuesday { get; private set; }
        //public bool Wednesday { get; private set; }
        //public bool Thursday { get; private set; }
        //public bool Friday { get; private set; }
        //public bool Saturday { get; private set; }
    }
}
