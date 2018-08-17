using Microsoft.AspNetCore.Mvc.Rendering;
using SaaSEqt.eShop.WebMVCBackend.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.WebMVCBackend.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ServiceCategory> ServiceCategories { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
