using Microsoft.AspNetCore.Mvc.Rendering;
using SaaSEqt.eShop.WebMVCBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.WebMVCBackend.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<ServiceCategory>> GetCategories(int page, int take, Guid siteId);
        Task AddCategory(Guid siteId, string name, string description, bool allowOnlineScheduling, int scheduleType);
    }
}
