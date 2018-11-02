using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaaSEqt.eShop.Services.Identity.API.Models
{

    public enum Continents
    {
        None, Africa, Asia, Australia, Europe, America
    }

    public enum ExperienceLevels
    {
        None, Novice, Intermediate, Advanced, Expert, Master
    }

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //[Required]
        public string CardNumber { get; set; }
        //[Required]
        public string SecurityNumber { get; set; }
        //[Required]
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string Expiration { get; set; }
        //[Required]
        public string CardHolderName { get; set; }
        public int CardType { get; set; }
        //[Required]
        public string Street { get; set; }
        //[Required]
        public string City { get; set; }
        //[Required]
        public string State { get; set; }
        //[Required]
        public string Country { get; set; }
        //[Required]
        public string ZipCode { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return Name + " " + LastName; } }

        public byte[] AvatarImage { get; set; }

        public string AvatarImageFileName { get; set; }

        [NotMapped]
        public string AvatarImageUri { get; set; }

        public static ApplicationUser Empty()
        {
            ApplicationUser user = new ApplicationUser();

            user.CardNumber = string.Empty;
            user.SecurityNumber = string.Empty;
            user.Expiration = string.Empty;
            user.CardHolderName = string.Empty;
            user.Street = string.Empty;
            user.City = string.Empty;
            user.State = string.Empty;
            user.Country = string.Empty;
            user.ZipCode = string.Empty;
            user.Name = string.Empty;
            user.LastName = string.Empty;
            user.AvatarImage = new byte[0];
            user.AvatarImageFileName = string.Empty;

            return user;
        }
    }
}
