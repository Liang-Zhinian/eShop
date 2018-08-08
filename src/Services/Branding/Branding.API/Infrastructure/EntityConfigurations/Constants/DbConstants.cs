using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SaaSEqt.eShop.Services.Branding.API.Infrastructure.EntityConfigurations.Constants
{
    public class DbConstants
    {
        public static string Schema = "book2.business";

        public static string KeyType = "uniqueidentifier";
        public static string String10 = "NVarchar(10)";
        public static string String36 = "NVarchar(36)";
        public static string String255 = "NVarchar(255)";
        public static string String1000 = "NVarchar(1000)";
        public static string String2000 = "NVarchar(2000)";
        public static string String4000 = "NVarchar(4000)";


        public static string ServiceItemTable { get; set; }
        public static string ServiceCategoryTable { get; set; }

        public static string ScheduleTypeTable { get; set; }
        public static string AvailabilityTable { get; set; }
        public static string UnavailabilityTable { get; set; }

        static DbConstants()
        {
            ScheduleTypeTable = "ScheduleType";
            AvailabilityTable = "Availability";
            UnavailabilityTable = "Unavailability";
            ServiceItemTable = "ServiceItem";
            ServiceCategoryTable = "ServiceCategory";

            KeyType = "char(36)";
            String10 = "varchar(10)";
            String36 = "varchar(36)";
            String255 = "varchar(255)";
            String1000 = "varchar(1000)";
            String2000 = "varchar(2000)";
            String4000 = "varchar(4000)";

        }
    }
}
