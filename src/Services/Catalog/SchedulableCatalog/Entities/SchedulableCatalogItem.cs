using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaaSEqt.eShop.Services.SchedulableCatalog.Entities
{
    public class SchedulableCatalogItem //: CatalogItem // : AggregateRoot
    {
        private SchedulableCatalogItem()
        {
            Id = Guid.NewGuid();
        }

        public SchedulableCatalogItem(Guid siteId, string name, string description, int defaultTimeLength, double price, Guid schedulableCatalogTypeId, int industryStandardCategoryId)
            : this()
        {
            SiteId = siteId;
            Name = name;
            Description = description;
            DefaultTimeLength = defaultTimeLength;
            SchedulableCatalogTypeId = schedulableCatalogTypeId;
            Price = price;
            IndustryStandardCategoryId = industryStandardCategoryId;


            //var serviceItemCreatedEvent = new ServiceItemCreatedEvent(Id,
            // name,
            // description,
            // defaultTimeLength,
            // price,
            // serviceCategoryId,
            // siteId,
            // industryStandardCategoryId
            //);
            //ApplyChange(serviceItemCreatedEvent);
        }

        #region public properties

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int DefaultTimeLength { get; private set; }

        public Guid SchedulableCatalogTypeId { get; private set; }
        public virtual SchedulableCatalogType SchedulableCatalogType { get; private set; }

        public int IndustryStandardCategoryId { get; private set; }

        public double Price { get; private set; }

        public bool AllowOnlineScheduling { get; private set; } // or Show in Consumer Mode?

        public double TaxRate { get; private set; }

        public double TaxAmount => Price * TaxRate;

        public Guid SiteId { get; private set; }

        public virtual ICollection<Availability> Availibilities { get; private set; }
        public virtual ICollection<Unavailability> Unavailabilities { get; private set; }

        #endregion

        #region [Command Methods]

        public Availability AddAvailability(Guid staffId, Guid locationId, DateTime startTime, DateTime endTime, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, DateTime bookableEndTime)
        {
            Availability availability = new Availability(this.SiteId, staffId, this.Id, locationId, startTime, endTime, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, bookableEndTime);

            if (Availibilities == null) Availibilities = new ObservableCollection<Availability>();

            Availibilities.Add(availability);

            return availability;
        }

        public Unavailability AddUnavailability(Guid staffId, Guid locationId, DateTime startTime, DateTime endTime, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, string description)
        {
            Unavailability unavailability = new Unavailability(this.SiteId, staffId, this.Id, locationId, startTime, endTime, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, description);

            if (Unavailabilities == null) Unavailabilities = new ObservableCollection<Unavailability>();

            Unavailabilities.Add(unavailability);

            return unavailability;
        }


        #endregion
    }
}
