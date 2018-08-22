namespace SaaSEqt.eShop.Business.Domain.Model.Security
{
    public class ContactInformation //: ValueObject<ContactInformation>
    {
        public string ContactName { get; set; }

        public string EmailAddress { get; set; }

        public string PrimaryTelephone { get; set; }

        public string SecondaryTelephone { get; set; }

        private ContactInformation()
        {
        }

        public ContactInformation(string contactName, string primaryTelephone, string secondaryTelephone, string emailAddress)
        {
            ContactName = contactName;
            PrimaryTelephone = primaryTelephone;
            SecondaryTelephone = secondaryTelephone;
            EmailAddress = emailAddress;
        }

        internal static ContactInformation Empty()
        {
            return new ContactInformation();
        }
    }
}