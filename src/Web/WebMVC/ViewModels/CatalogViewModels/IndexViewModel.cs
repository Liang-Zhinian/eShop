using Microsoft.AspNetCore.Mvc.Rendering;
using SaaSEqt.eShop.WebMVC.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.WebMVC.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public Guid? BrandFilterApplied { get; set; }
        public Guid? TypesFilterApplied { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
