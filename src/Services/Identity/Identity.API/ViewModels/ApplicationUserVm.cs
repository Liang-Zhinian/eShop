using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.ViewModels
{

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUserVm
    {
        public Guid Id { get; set; }

        public string Street { get; set; }
       
        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public byte[] AvatarImage { get; set; }

        public string AvatarImageFileName { get; set; }

        public string AvatarImageUri { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
