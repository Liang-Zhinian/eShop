using System;
using CqrsFramework.Events;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Staffs
{
    public class UnavailabilityAddedByStaffEvent: ScheduleItemEvent, IEvent
    {
        public UnavailabilityAddedByStaffEvent(Guid id, Guid siteId, Guid staffId, Guid serviceItemId, Guid locationId, DateTime startTime, DateTime endTime, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, string description)
            : base(id, siteId, staffId, serviceItemId, locationId, startTime, endTime, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
