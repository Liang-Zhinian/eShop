using System.ComponentModel.DataAnnotations.Schema;

namespace Eva.eShop.Services.Identity.API.Models
{
    /// <summary>
    /// Gender. Male = 0, Female = 1, Other = 2, Unknown = 3
    /// </summary>
    public enum Gender
    {
        Male, Female, Other, Unknown
    }

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
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string SecurityNumber { get; set; }
        [Required]
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string Expiration { get; set; }
        [Required]
        public string CardHolderName { get; set; }
        public int CardType { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }

        //[NotMapped]
        //public Gender Gender
        //{
        //    get => (Gender)GenderId;
        //    set => GenderId = (int)value;
        //}

        //public int GenderId { get; set; } = 3;

        //[NotMapped]
        //public string FullName { get { return Name + " " + LastName; } }

        //public byte[] AvatarImage { get; set; }

        //public string AvatarImageFileName { get; set; }

        //[NotMapped]
        //public string AvatarImageUri { get; set; }

        //public ExternalAccounts ExternalAccounts { get; set; }

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
            //user.AvatarImage = new byte[0];
            //user.AvatarImageFileName = string.Empty;
            //user.Gender = Gender.Unknown;

            return user;
        }
    }

    public class ExternalAccounts
    {
        public Guid Id { get; set; }

        public string FacebookEmail { get; set; }
        public string TwitterUsername { get; set; }
        public string WechatOpenId { get; set; }
        public string AlipayUserId { get; set; }
        public string Pay2OpenId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
