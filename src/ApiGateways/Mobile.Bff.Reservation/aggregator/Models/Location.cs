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

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Image { get; private set; }

        public string ImageUri { get; set; }

        public ContactInformation ContactInformation { get; private set; }

        public Address Address { get; private set; }

        public Geolocation Geolocation { get; private set; }

        public bool Active { get; private set; }

        public Guid SiteId { get; private set; }

        public ICollection<LocationImage> AdditionalLocationImages { get; private set; }

        #endregion

    }
}
