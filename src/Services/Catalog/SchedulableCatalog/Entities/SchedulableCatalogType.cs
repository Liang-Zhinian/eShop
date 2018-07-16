using System;

namespace SaaSEqt.eShop.Services.SchedulableCatalog.Entities
{
    public class SchedulableCatalogType // : CatalogType // : AggregateRoot
    {

        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool AllowOnlineScheduling { get; private set; }

        public int ScheduleTypeId { get; private set; }
        public virtual ScheduleType ScheduleType { get; private set; }

        public Guid SiteId { get; private set; }

        private SchedulableCatalogType()
        {
            Id = Guid.NewGuid();
        }

        public SchedulableCatalogType(Guid siteId, string name, string description, bool allowOnlineScheduling, int scheduleType) : this()
        {
            SiteId = siteId;
            Name = name;
            Description = description;
            AllowOnlineScheduling = allowOnlineScheduling;
            ScheduleTypeId = scheduleType;

            //var serviceCategoryCreatedEvent = new ServiceCategoryCreatedEvent(Id,
            //                                                                  name,
            //                                                                  description,
            //                                                                  cancelOffset,
            //                                                                  scheduleType,
            //                                                                  siteId
            //                                                                 );
            //ApplyChange(serviceCategoryCreatedEvent);
        }
    }
}