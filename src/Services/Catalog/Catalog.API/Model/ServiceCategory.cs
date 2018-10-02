using System;

namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class ServiceCategory
    {

        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowOnlineScheduling { get; set; }

        public int ScheduleTypeId { get; set; }
        public virtual ScheduleType ScheduleType { get; set; }

        public Guid SiteId { get; set; }

        private ServiceCategory()
        {
            Id = Guid.NewGuid();
        }

        public ServiceCategory(Guid siteId, Guid serviceCategoryId, string name, string description, bool allowOnlineScheduling, int scheduleType) : this()
        {
            SiteId = siteId;
            Id = serviceCategoryId;
            Name = name;
            Description = description;
            AllowOnlineScheduling = allowOnlineScheduling;
            ScheduleTypeId = scheduleType;
        }
    }
}