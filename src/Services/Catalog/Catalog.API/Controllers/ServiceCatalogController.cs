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
using SaaSEqt.eShop.Services.Catalog.API.Model;
using SaaSEqt.eShop.Services.Catalog.API;
using Catalog.API.IntegrationEvents;

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _catalogContext;
        private readonly CatalogSettings _settings;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public CatalogController(CatalogContext context, IOptionsSnapshot<CatalogSettings> settings, ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            _catalogContext = context ?? throw new ArgumentNullException(nameof(context));
            _catalogIntegrationEventService = catalogIntegrationEventService ?? throw new ArgumentNullException(nameof(catalogIntegrationEventService));

            _settings = settings.Value;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        #region [service items]

        [HttpGet]
        [Route("sites/{siteId:guid}/serviceitems/{serviceItemId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceItems(Guid siteId, Guid serviceItemId)
        {
            if (siteId == Guid.Empty || serviceItemId == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.ServiceItems
                                            .SingleOrDefaultAsync(si => si.SiteId == siteId 
                                                                  && si.Id == serviceItemId);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        //GET api/v1/[controller]/sites/siteId/servicecategories/{serviceCategoryId:guid}/serviceitems/{serviceItemId:guid}
        [HttpGet]
        [Route("sites/{siteId:guid}/servicecategories/{serviceCategoryId:guid}/serviceitems")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceItems(Guid siteId, Guid? serviceCategoryId, Guid? serviceItemId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems
                                                               .Where(y => y.SiteId.Equals(siteId));
                                                               //.Include(y => y.ServiceCategory);

            if (serviceCategoryId.HasValue)
            {
                root = root.Where(ci => ci.ServiceCategoryId == serviceCategoryId);

                if (root == null)
                {
                    return BadRequest();
                }
            }

            if (serviceItemId.HasValue)
            {
                var item = await root.SingleOrDefaultAsync(ci => ci.Id == serviceItemId);

                if (item != null)
                {
                    return Ok(item);
                }

                return BadRequest();
            }

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
        public async Task<IActionResult> CreateServiceItem([FromBody]ServiceItem schedulableCatalogItem)
        {
            var newItem = new ServiceItem(schedulableCatalogItem.SiteId,
                                          Guid.NewGuid(),
                                        schedulableCatalogItem.Name,
                                        schedulableCatalogItem.Description,
                                        schedulableCatalogItem.DefaultTimeLength,
                                        schedulableCatalogItem.Price,
                                        schedulableCatalogItem.ServiceCategoryId,
                                        schedulableCatalogItem.IndustryStandardCategoryName,
                                        schedulableCatalogItem.IndustryStandardSubcategoryName);
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
        public async Task<IActionResult> AddServiceCategory([FromBody]ServiceCategory schedulableCatalogType)
        {
            var newCategory = new ServiceCategory(
                schedulableCatalogType.SiteId,
                  Guid.NewGuid(),
                  schedulableCatalogType.Name,
                  schedulableCatalogType.Description,
                  schedulableCatalogType.AllowOnlineScheduling,
                  schedulableCatalogType.ScheduleTypeId);
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

        //GET api/v1/[controller]/[action]/siteId/{siteId:guid}
        [HttpGet]
        [Route("sites/{siteId:guid}/servicecategories")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceCategory>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceCategory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceCategories(Guid siteId, Guid? serviceCategoryId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceCategory>)_catalogContext.ServiceCategories
                                                                   .Where(y => y.SiteId.Equals(siteId));
                                                                   //.Include(y => y.ScheduleType);

            if (serviceCategoryId.HasValue)
            {
                root = root.Where(ci => ci.SiteId == siteId);
                var item = await root.SingleOrDefaultAsync(ci => ci.Id == serviceCategoryId);

                if (item != null)
                {
                    return Ok(item);
                }

                return NotFound();
            }

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
