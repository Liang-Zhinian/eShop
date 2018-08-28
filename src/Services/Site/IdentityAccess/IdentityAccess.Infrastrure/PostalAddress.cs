using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaaSEqt.IdentityAccess.Infrastructure
{
    [NotMapped]
    public class PostalAddress
    {

        public string City { get; set; }

        public string CountryCode { get; set; }

        public string PostalCode { get; set; }

        public string StateProvince { get; set; }

        public string StreetAddress { get; set; }

        public string StreetAddress2 { get; set; }
    }
}
