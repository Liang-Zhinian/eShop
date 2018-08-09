namespace SaaSEqt.eShop.Services.Business.Model
{
    public class Geolocation
    {
        public Geolocation()
        {

        }

        public Geolocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public static Geolocation Empty(){
            return new Geolocation();
        }
    }
}