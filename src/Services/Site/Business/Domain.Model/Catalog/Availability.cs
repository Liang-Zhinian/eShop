using System;
using System.Collections.Generic;

namespace SaaSEqt.eShop.Business.Domain.Model.Catalog
{
    public class Availability :ScheduleItem
    {
        protected Availability()
        {
        }

        public Availability(Guid siteId, Guid staffId, Guid serviceItemId, Guid locationId, DateTime startTime, DateTime endTime, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, DateTime bookableEndTime)
            : base(siteId, staffId, serviceItemId, locationId, startTime, endTime/*, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday*/)
        {
            this.BookableEndDateTime = bookableEndTime;
            this.Sunday = Sunday;
            this.Monday = Monday;
            this.Tuesday = Tuesday;
            this.Wednesday = Wednesday;
            this.Thursday = Thursday;
            this.Friday = Friday;
            this.Saturday = Saturday;
        }

        ///
        public DateTime BookableEndDateTime { get; set; }

        /// Staff, teacher, or trainer
        //public Guid StaffId { get; private set; }
        //public virtual Staff Staff { get; private set; }

        //public ICollection<Program> Programs { get; private set; }

        // DaysOfWeek?
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }
}
