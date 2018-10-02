using System;

namespace SaaSEqt.eShop.Services.Business.API.Application.Events.Staffs
{
    public class StaffCreatedEvent : BaseEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsMale { get; set; }

        public StaffCreatedEvent(Guid id, string firstName, string lastName, bool isMale)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsMale = isMale;
        }
    }
}