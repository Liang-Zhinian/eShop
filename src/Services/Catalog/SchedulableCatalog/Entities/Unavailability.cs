using System;
namespace SaaSEqt.eShop.Services.SchedulableCatalog.Entities
{
    public class Unavailability : ScheduleItem
    {

        protected Unavailability()
        {
        }

        public Unavailability(Guid siteId, Guid staffId, Guid schedulableCatalogItemId, Guid locationId, DateTime startTime, DateTime endTime, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, string description)
            : base(siteId, staffId, schedulableCatalogItemId, locationId, startTime, endTime/*, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday*/)
        {
            this.Description = description;
            this.Sunday = Sunday;
            this.Monday = Monday;
            this.Tuesday = Tuesday;
            this.Wednesday = Wednesday;
            this.Thursday = Thursday;
            this.Friday = Friday;
            this.Saturday = Saturday;
        }

        public string Description { get; private set; }

        // DaysOfWeek?
        public bool Sunday { get; private set; }
        public bool Monday { get; private set; }
        public bool Tuesday { get; private set; }
        public bool Wednesday { get; private set; }
        public bool Thursday { get; private set; }
        public bool Friday { get; private set; }
        public bool Saturday { get; private set; }
    }
}
