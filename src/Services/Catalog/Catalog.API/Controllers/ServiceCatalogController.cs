using SaaSEqt.eShop.Services.Catalog.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Catalog.API.Infrastructure;
using SaaSEqt.eShop.Services.Catalog.API.IntegrationEvents.Events;
using SaaSEqt.eShop.Services.Catalog.API.Model;
using SaaSEqt.eShop.Services.Catalog.API.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.Catalog.API;
using Catalog.API.IntegrationEvents;
using Microsoft.AspNetCore.Authorization;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Administrator,Manager")]
    public class ServiceCatalogController : ControllerBase
    {
        private readonly CatalogContext _catalogContext;
        private readonly CatalogSettings _settings;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public ServiceCatalogController(CatalogContext context, IOptionsSnapshot<CatalogSettings> settings, ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            _catalogContext = context ?? throw new ArgumentNullException(nameof(context));
            _catalogIntegrationEventService = catalogIntegrationEventService ?? throw new ArgumentNullException(nameof(catalogIntegrationEventService));

            _settings = settings.Value;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        #region [service items]

        // GET api/v1/serviceitems[?siteIds=xxxx,xxx&searchText=somename&pageSize=10&pageIndex=3]
        [AllowAnonymous]
        [HttpGet]
        [Route("serviceitems")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SeriviceItems([FromQuery]string siteIds, [FromQuery]string searchText, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems;

            if (!string.IsNullOrEmpty(siteIds))
            {
                var guidIds = siteIds.Split(',')
                                .Select(id => (Ok: Guid.TryParse(id, out Guid x), Value: x));
                if (!guidIds.All(nid => nid.Ok))
                {
                    return BadRequest("siteids value invalid. Must be comma-separated list of guids (uuids)");
                }

                var idsToSelect = guidIds.Select(id => id.Value);
                root = root.Where(ci => idsToSelect.Contains(ci.SiteId));
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                root = root.Where(y => y.Name.Contains(searchText));
            }

            var totalItems = await root.LongCountAsync();

            var itemsOnPage = root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedItemsViewModel<ServiceItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("serviceitems/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceItems(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.ServiceItems
                                            .SingleOrDefaultAsync(si => si.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        //GET api/v1/[controller]/sites/siteId/servicecategories/{serviceCategoryId:guid}/serviceitems
        [AllowAnonymous]
        [HttpGet]
        [Route("sites/{siteId:guid}/servicecategories/{serviceCategoryId:guid}/serviceitems")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceItems(Guid siteId, Guid serviceCategoryId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems
                                                               .Where(y => y.SiteId.Equals(siteId)
                                                                      && y.ServiceCategoryId == serviceCategoryId);

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedItemsViewModel<ServiceItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("sites/{siteId:guid}/serviceitems")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SeriviceItems(Guid siteId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems.Where(y => y.SiteId.Equals(siteId));

            var totalItems = await root.LongCountAsync();

            var itemsOnPage = root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedItemsViewModel<ServiceItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        private IActionResult GetItemsBySiteIds(string siteIds, string searchText = null)
        {
            var guidIds = siteIds.Split(',')
                            .Select(id => (Ok: Guid.TryParse(id, out Guid x), Value: x));
            if (!guidIds.All(nid => nid.Ok))
            {
                return BadRequest("siteids value invalid. Must be comma-separated list of guids (uuids)");
            }

            var idsToSelect = guidIds.Select(id => id.Value);
            var root = _catalogContext.ServiceItems
                                       .Where(ci => idsToSelect.Contains(ci.SiteId));
            if(!string.IsNullOrEmpty(searchText)){
                root = root.Where(y => y.Name.Contains(searchText));
            }    
                var items = root.ToList();

            return Ok(items);

        }

        private IActionResult GetItemsByIds(Guid siteId, string ids)
        {
            var numIds = ids.Split(',')
                            .Select(id => (Ok: Guid.TryParse(id, out Guid x), Value: x));
            if (!numIds.All(nid => nid.Ok))
            {
                return BadRequest("ids value invalid. Must be comma-separated list of numbers");
            }

            var idsToSelect = numIds.Select(id => id.Value);
            var items = _catalogContext.ServiceItems
                                      .Where(ci => ci.SiteId.Equals(siteId) && idsToSelect.Contains(ci.Id)).ToList();

            return Ok(items);

        }

        //PUT api/v1/[controller]/serviceitems
        [Route("serviceitems")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateServiceItem([FromBody]ServiceItem serviceItemToUpdate)
        {
            var serviceItem = await _catalogContext.ServiceItems
                                                   .SingleOrDefaultAsync(i => i.SiteId == serviceItemToUpdate.SiteId
                                                                         && i.Id == serviceItemToUpdate.Id);

            if (serviceItem == null)
            {
                return NotFound(new { Message = $"Item with id {serviceItemToUpdate.Id} not found." });
            }

            var oldPrice = serviceItem.Price;
            var raiseServiceItemPriceChangedEvent = oldPrice != serviceItemToUpdate.Price;


            // Update current product
            serviceItem = serviceItemToUpdate;
            _catalogContext.ServiceItems.Update(serviceItem);

            //if (raiseServiceItemPriceChangedEvent) // Save product's data and publish integration event through the Event Bus if price has changed
            //{
            //    //Create Integration Event to be published through the Event Bus
            //    var priceChangedEvent = new ProductPriceChangedIntegrationEvent(serviceItem.Id, serviceItemToUpdate.Price, oldPrice);

            //    // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
            //    await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent);

            //    // Publish through the Event Bus and mark the saved event as published
            //    await _catalogIntegrationEventService.PublishThroughEventBusAsync(priceChangedEvent);
            //}
            //else // Just save the updated product because the Product's Price hasn't changed.
            //{
            //    await _catalogContext.SaveChangesAsync();
            //}

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ServiceItems), new { siteId = serviceItemToUpdate.SiteId, serviceItemId = serviceItemToUpdate.Id }, null);
        }

        //POST api/v1/[controller]/serviceitems
        [HttpPost]
        [Route("serviceitems")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateServiceItem([FromBody]ServiceItem serviceItemToCreate)
        {
            var newItem = new ServiceItem(serviceItemToCreate.SiteId,
                                          Guid.NewGuid(),
                                        serviceItemToCreate.Name,
                                        serviceItemToCreate.Description,
                                        serviceItemToCreate.DefaultTimeLength,
                                        serviceItemToCreate.Price,
                                        serviceItemToCreate.ServiceCategoryId,
                                        serviceItemToCreate.IndustryStandardCategoryName,
                                        serviceItemToCreate.IndustryStandardSubcategoryName);
            _catalogContext.ServiceItems.Add(newItem);
            await _catalogContext.SaveChangesAsync();
            //ServiceItemCreatedEvent serviceItemCreatedEvent = new ServiceItemCreatedEvent(newItem.SiteId,
            //                                                                              newItem.Id,
            //                                                                              newItem.Name,
            //                                                                              newItem.Description,
            //                                                                              newItem.DefaultTimeLength,
            //                                                                              newItem.Price,
            //                                                                              newItem.AllowOnlineScheduling,
            //                                                                              newItem.ServiceCategoryId,
            //                                                                              newItem.IndustryStandardCategoryName,
            //                                                                              newItem.IndustryStandardSubcategoryName);


            //await _catalogIntegrationEventService.PublishThroughEventBusAsync(serviceItemCreatedEvent);


            return CreatedAtAction(nameof(ServiceItems), new { siteId = newItem.SiteId, serviceItemId = newItem.Id }, null);
        }

        #endregion [service items]

        #region [service categories]

        //POST api/v1/[controller]/servicecategories
        [HttpPost]
        [Route("servicecategories")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateServiceCategory([FromBody]ServiceCategory serviceCategoryToCreate)
        {
            var newCategory = new ServiceCategory(
                serviceCategoryToCreate.SiteId,
                  Guid.NewGuid(),
                  serviceCategoryToCreate.Name,
                  serviceCategoryToCreate.Description,
                  serviceCategoryToCreate.AllowOnlineScheduling,
                  serviceCategoryToCreate.ScheduleTypeId);
            _catalogContext.ServiceCategories.Add(newCategory);
            await _catalogContext.SaveChangesAsync();
            //ServiceCategoryCreatedEvent serviceCategoryCreatedEvent = new ServiceCategoryCreatedEvent(newCategory.SiteId,
            //newCategory.Id,
            //newCategory.Name,
            //newCategory.Description,
            //newCategory.AllowOnlineScheduling,
            //newCategory.ScheduleTypeId);


            //await _catalogIntegrationEventService.PublishThroughEventBusAsync(serviceCategoryCreatedEvent);


            return CreatedAtAction(nameof(ServiceCategories), new { siteId = newCategory.SiteId, serviceCategoryId = newCategory.Id }, null);
        }

        [HttpPut]
        [Route("servicecategories")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateServiceCategory([FromBody]ServiceCategory serviceCategoryToUpdate)
        {
            var item = await _catalogContext.ServiceCategories
                                                   .SingleOrDefaultAsync(i => i.SiteId == serviceCategoryToUpdate.SiteId
                                                                         && i.Id == serviceCategoryToUpdate.Id);

            if (item == null)
            {
                return NotFound(new { Message = $"Category with id {serviceCategoryToUpdate.Id} not found." });
            }

            item.Name = serviceCategoryToUpdate.Name;
            item.Description = serviceCategoryToUpdate.Description;
            item.AllowOnlineScheduling = serviceCategoryToUpdate.AllowOnlineScheduling;

            // Update current product
            _catalogContext.ServiceCategories.Update(item);
            await _catalogContext.SaveChangesAsync();

            //ServiceCategoryCreatedEvent serviceCategoryCreatedEvent = new ServiceCategoryCreatedEvent(newCategory.SiteId,
            //newCategory.Id,
            //newCategory.Name,
            //newCategory.Description,
            //newCategory.AllowOnlineScheduling,
            //newCategory.ScheduleTypeId);


            //await _catalogIntegrationEventService.PublishThroughEventBusAsync(serviceCategoryCreatedEvent);


            return CreatedAtAction(nameof(ServiceCategories), new { siteId = item.SiteId, serviceCategoryId = item.Id }, null);
        }

        //GET api/v1/[controller]/sites/{siteId:guid}/servicecategories/{id:guid}
        [AllowAnonymous]
        [HttpGet]
        [Route("servicecategories/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceCategory), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceCategories(Guid id)
        {
            var item = await _catalogContext.ServiceCategories
                                            .SingleOrDefaultAsync(y => y.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }


        //GET api/v1/[controller]/sites/{siteId:guid}/servicecategories
        [AllowAnonymous]
        [HttpGet]
        [Route("sites/{siteId:guid}/servicecategories")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceCategory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetServiceCategoriesBySiteId(Guid siteId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceCategory>)_catalogContext.ServiceCategories
                                                                   .Where(y => y.SiteId.Equals(siteId));

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedItemsViewModel<ServiceCategory>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        #endregion [service categories]
    }
}
