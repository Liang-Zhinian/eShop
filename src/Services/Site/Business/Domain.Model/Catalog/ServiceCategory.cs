using System;

namespace SaaSEqt.eShop.Business.Domain.Model.Catalog
{
    public class ServiceCategory // : CatalogType // : AggregateRoot
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

        public ServiceCategory(Guid siteId, string name, string description, bool allowOnlineScheduling, int scheduleType) : this()
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