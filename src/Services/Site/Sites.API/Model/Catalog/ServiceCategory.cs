﻿using System;

namespace SaaSEqt.eShop.Services.Sites.API.Model.Catalog
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

        public ServiceCategory(Guid siteId, Guid id, string name, string description, bool allowOnlineScheduling, int scheduleType)
        {
            SiteId = siteId;
            Id = id;
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