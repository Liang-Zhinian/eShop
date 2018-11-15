﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaaSEqt.eShop.WebMVC.ViewModels.Pagination;
using SaaSEqt.eShop.WebMVC.Services;
using SaaSEqt.eShop.WebMVC.ViewModels.CatalogViewModels;
using Microsoft.AspNetCore.Http;

namespace SaaSEqt.eShop.WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private ICatalogService _catalogSvc;

        public CatalogController(ICatalogService catalogSvc) => 
            _catalogSvc = catalogSvc;

        public async Task<IActionResult> Index(Guid? BrandFilterApplied, Guid? TypesFilterApplied, int? page, [FromQuery]string errorMsg)
        {
            var itemsPage = 10;
            var catalog = await _catalogSvc.GetCatalogItems(page ?? 0, itemsPage, BrandFilterApplied, TypesFilterApplied);
            var vm = new IndexViewModel()
            {
                CatalogItems = catalog.Data,
                Brands = await _catalogSvc.GetBrands(),
                Types = await _catalogSvc.GetTypes(),
                BrandFilterApplied = BrandFilterApplied ?? Guid.Empty,
                TypesFilterApplied = TypesFilterApplied ?? Guid.Empty,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = catalog.Data.Count,
                    TotalItems = catalog.Count, 
                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / itemsPage))
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            ViewBag.BasketInoperativeMsg = errorMsg;

            return View(vm);
        }
    }
}