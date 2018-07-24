using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SaaSEqt.IdentityAccess.Api.ViewModel
{
    public class TenantViewModel
    {
        public TenantViewModel()
        {
        }

        public TenantViewModel(Guid id, 
                               string name, 
                               string description, 
                               string email,
                               string primaryTelephone, 
                               string secondaryTelephone,
                               string street,
                               string street2,
                               string city, 
                               string state,
                               string country,
                               string postalCode )
        {
            Id = id;
            Name = name;
            Description = description;
            Email = email;
            PrimaryTelephone = primaryTelephone;
            SecondaryTelephone = secondaryTelephone;
            State = state;
            City = city;
            Street = street;
            Street2 = street2;
            Country = country;
            PostalCode = postalCode;
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [MinLength(2)]
        [MaxLength(2000)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The MobilePhone is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("PrimaryTelephone")]
        public string PrimaryTelephone { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("SecondaryTelephone")]
        public string SecondaryTelephone { get; set; }

        [Required(ErrorMessage = "The Street is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Address")]
        public string Street { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Address (line 2)")]
        public string Street2 { get; set; }

        [Required(ErrorMessage = "The City is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "The State is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("State")]
        public string State { get; set; }

        [Required(ErrorMessage = "The Country is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Country")]
        public string Country { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("PostalCode")]
        public string PostalCode { get; set; }

    }
}
