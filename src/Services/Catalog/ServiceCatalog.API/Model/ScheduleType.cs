using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCatalog.API.Infrastructure.Exceptions;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Model
{
    //public class ScheduleTypeTable
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public string Label { get; set; }
    //}

    public class ScheduleType : Enumeration
    {
        //All = 0,
        //DropIn,
        //Enrollment,
        //Appointment,
        //Resource,
        //Media,
        //Arrival

        public static ScheduleType All = new ScheduleType(1, nameof(All).ToLowerInvariant());
        public static ScheduleType Appointment = new ScheduleType(2, nameof(Appointment).ToLowerInvariant());
        public static ScheduleType Resource = new ScheduleType(3, nameof(Resource).ToLowerInvariant());
        //public static ScheduleType DropIn = new ScheduleType(2, nameof(DropIn).ToLowerInvariant());
        //public static ScheduleType Enrollment = new ScheduleType(3, nameof(Enrollment).ToLowerInvariant());
        //public static ScheduleType Media = new ScheduleType(6, nameof(Media).ToLowerInvariant());
        //public static ScheduleType Arrival = new ScheduleType(7, nameof(Arrival).ToLowerInvariant());

        protected ScheduleType()
        {
        }

        public ScheduleType(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ScheduleType> List() =>
        new[] { All, Appointment, Resource, /*DropIn, Enrollment, Media, Arrival*/ };

        public static ScheduleType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new CatalogDomainException($"Possible values for ScheduleType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ScheduleType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new CatalogDomainException($"Possible values for ScheduleType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
