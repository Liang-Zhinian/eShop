using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SaaSEqt.IdentityAccess.Infrastructure.Mappings.Constants
{
    public static class DataBaseServer
    {
        public static readonly string SqlServer = "SqlServer";
        public static readonly string MySql = "MySql";
    }

    public class DbConstants
    {
        //public static string Schema = "book2business";

        public static string KeyType = "uniqueidentifier";
        public static string String10 = "NVarchar(10)";
        public static string String36 = "NVarchar(36)";
        public static string String255 = "NVarchar(255)";
        public static string String1000 = "NVarchar(1000)";
        public static string String2000 = "NVarchar(2000)";
        public static string String4000 = "NVarchar(4000)";

        public static string TenantTable { get; private set; }
        public static string RoleTable { get; private set; }
        public static string RoleMemberTable { get; private set; }

        public static string GroupTable { get; private set; }
        public static string GroupMemberTable { get; private set; }
        public static string UserTable { get; private set; }

        public static string TimeZoneTable { get; set; }
        public static string RegionTable { get; set; }


        static DbConstants()
        {
            TenantTable = "Tenant";
            RoleTable = "Role";
            RoleMemberTable = "RoleMember";

            GroupTable = "Group";
            GroupMemberTable = "GroupMember";
            UserTable = "User";

            TimeZoneTable = "TimeZone";
            RegionTable = "Region";

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
