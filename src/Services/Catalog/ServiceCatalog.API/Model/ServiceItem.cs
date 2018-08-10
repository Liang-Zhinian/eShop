using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Model
{
    public class ServiceItem //: CatalogItem // : AggregateRoot
    {
        private ServiceItem()
        {
            Id = Guid.NewGuid();
        }

        public ServiceItem(Guid siteId, string name, string description, int defaultTimeLength, double price, Guid serviceCategoryId, string industryStandardCategoryName, string industryStandardSubcategoryName)
            : this()
        {
            SiteId = siteId;
            Name = name;
            Description = description;
            DefaultTimeLength = defaultTimeLength;
            ServiceCategoryId = serviceCategoryId;
            Price = price;
            IndustryStandardCategoryName = industryStandardCategoryName;
            IndustryStandardSubcategoryName = industryStandardSubcategoryName;


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

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DefaultTimeLength { get; set; }

        public Guid ServiceCategoryId { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; }

        public string IndustryStandardCategoryName { get; set; }

        public string IndustryStandardSubcategoryName { get; set; }

        public double Price { get; set; }

        public bool AllowOnlineScheduling { get; set; } // or Show in Consumer Mode?

        public double TaxRate { get; set; }

        public double TaxAmount => Price * TaxRate;

        public Guid SiteId { get; set; }

        public virtual ICollection<Availability> Availibilities { get; set; }
        public virtual ICollection<Unavailability> Unavailabilities { get; set; }

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
