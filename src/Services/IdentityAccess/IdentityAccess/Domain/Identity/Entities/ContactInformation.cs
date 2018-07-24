
namespace SaaSEqt.IdentityAccess.Domain.Identity.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using SaaSEqt.Common.Domain.Model;

    [NotMapped]
    public class ContactInformation : ValueObject
    {
        public ContactInformation(
                EmailAddress emailAddress,
                PostalAddress postalAddress,
                Telephone primaryTelephone,
                Telephone secondaryTelephone)
        {
            this.EmailAddress = emailAddress;
            this.PostalAddress = postalAddress;
            this.PrimaryTelephone = primaryTelephone;
            this.SecondaryTelephone = secondaryTelephone;
        }

        public ContactInformation(ContactInformation contactInformation)
            : this(contactInformation.EmailAddress,
                   contactInformation.PostalAddress,
                   contactInformation.PrimaryTelephone,
                   contactInformation.SecondaryTelephone)
        {
        }

        internal ContactInformation()
        {
        }

        public EmailAddress EmailAddress { get; private set; }

        public PostalAddress PostalAddress { get; private set; }

        public Telephone PrimaryTelephone { get; private set; }

        public Telephone SecondaryTelephone { get; private set; }

        public ContactInformation ChangeEmailAddress(EmailAddress emailAddress)
        {
            return new ContactInformation(
                    emailAddress,
                    this.PostalAddress,
                    this.PrimaryTelephone,
                    this.SecondaryTelephone);
        }

        public ContactInformation ChangePostalAddress(PostalAddress postalAddress)
        {
            return new ContactInformation(
                   this.EmailAddress,
                   postalAddress,
                   this.PrimaryTelephone,
                   this.SecondaryTelephone);
        }

        public ContactInformation ChangePrimaryTelephone(Telephone telephone)
        {
            return new ContactInformation(
                   this.EmailAddress,
                   this.PostalAddress,
                   telephone,
                   this.SecondaryTelephone);
        }

        public ContactInformation ChangeSecondaryTelephone(Telephone telephone)
        {
            return new ContactInformation(
                   this.EmailAddress,
                   this.PostalAddress,
                   this.PrimaryTelephone,
                   telephone);
        }

        public override string ToString()
        {
            return "ContactInformation [emailAddress=" + EmailAddress
                    + ", postalAddress=" + PostalAddress
                    + ", primaryTelephone=" + PrimaryTelephone
                    + ", secondaryTelephone=" + SecondaryTelephone + "]";
        }

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.EmailAddress;
            yield return this.PostalAddress;
            yield return this.PrimaryTelephone;
            yield return this.SecondaryTelephone;
        }
    }
}
