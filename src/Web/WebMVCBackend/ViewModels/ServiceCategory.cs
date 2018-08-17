using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.WebMVCBackend.ViewModels
{
    public class ServiceCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowOnlineScheduling { get; set; }
        public int ScheduleTypeId { get; set; }
        public Guid SiteId { get; set; }
    }
}
