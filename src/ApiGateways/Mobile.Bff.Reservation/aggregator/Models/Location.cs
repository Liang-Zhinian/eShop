using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaaSEqt.eShop.Mobile.Reservation.HttpAggregator.Models
{
    public class Location
    {
        #region public properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string ImageUri { get; set; }

        public ContactInformation ContactInformation { get; set; }

        public Address Address { get; set; }

        public Geolocation Geolocation { get; set; }

        public bool Active { get; set; }

        public Guid SiteId { get; set; }

        public ICollection<LocationImage> AdditionalLocationImages { get; set; }

        #endregion

    }
}
