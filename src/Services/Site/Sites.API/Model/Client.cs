using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaaSEqt.eShop.Services.Sites.API.Model
{
    public class Client
    {
        public Client()
        {
        }

        public Client(
            Guid id, 
            string firstName, 
            string lastName, 
            string emailAddress, 
            string mobilePhone,
            string homePhone,
            string workPhone,
            Address address,
            Gender gender,
            DateTime birthdate,
            string notes,
            byte[] avatarImage
        )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            MobilePhone = mobilePhone;
            HomePhone = homePhone;
            WorkPhone = workPhone;
            Address = new Address(address.Street, address.City, address.StateProvince, address.ZipCode, address.Country);
            _genderId = gender.Id;
            Birthdate = birthdate;
            Notes = notes;
            AvatarImage = avatarImage;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; }}
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public Address Address { get; set; }

        private int _genderId;
        public Gender Gender { get; set; }

        public DateTime? Birthdate { get; set; }
        public DateTime? DateOfFirstAppointment { get; set; }
        public string Notes { get; set; }
        public byte[] AvatarImage { get; set; }

        public Guid SiteId { get; set; }
    }
}
