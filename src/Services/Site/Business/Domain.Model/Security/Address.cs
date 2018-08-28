namespace SaaSEqt.eShop.Business.Domain.Model.Security
{
    public class Address
    {
        private Address()
        {

        }

        public Address(string street, string city, string stateProvince, string zipCode, string country)
        {
            Street = street;
            City = city;
            StateProvince = stateProvince;
            ZipCode = zipCode;
            Country = country;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public static Address Empty(){
            return new Address();
        }
    }
}