using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.WebMVCBackend.Services;
using SaaSEqt.eShop.WebMVCBackend.ViewModels;
using SaaSEqt.eShop.WebMVCBackend.ViewModels.CatalogViewModels;
using SaaSEqt.eShop.WebMVCBackend.ViewModels.Pagination;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.WebMVCBackend.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categorySvc;

        public CategoryController(ICategoryService categorySvc)
        {
            _categorySvc = categorySvc;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int? page, [FromQuery]string errorMsg)
        {
            var itemsPage = 10;
            var categories = await _categorySvc.GetCategories(page ?? 0, itemsPage, Guid.Parse("5e61a19c-c6de-4217-ba28-f2962f892275"));
            var vm = new IndexViewModel()
            {
                ServiceCategories = categories,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            ViewBag.BasketInoperativeMsg = errorMsg;

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            return View(new ServiceCategory());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCategory serviceCategory){
            await _categorySvc.AddCategory(serviceCategory.SiteId, 
                                           serviceCategory.Name, 
                                           serviceCategory.Description, 
                                           serviceCategory.AllowOnlineScheduling, 
                                           serviceCategory.ScheduleTypeId);

            return View();
        }
    }
}
