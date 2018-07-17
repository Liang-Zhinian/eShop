﻿using ServiceCatalog.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure;
using SaaSEqt.eShop.Services.ServiceCatalog.API.IntegrationEvents.Events;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;
using SaaSEqt.eShop.Services.ServiceCatalog.API.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;

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

        //POST api/v1/[controller]/serviceitems
        [HttpGet]
        [Route("[action]/type/{catalogTypeId}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindServiceItems(Guid? catalogTypeId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems;

            if (catalogTypeId.HasValue)
            {
                root = root.Where(ci => ci.SchedulableCatalogTypeId == catalogTypeId);
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

        //POST api/v1/[controller]/serviceitems
        [HttpPost]
        [Route("serviceitems")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddServiceItem([FromBody]ServiceItem schedulableCatalogItem)
        {
            var item = new ServiceItem(schedulableCatalogItem.SiteId,
                                                  schedulableCatalogItem.Name,
                                                  schedulableCatalogItem.Description,
                                                  schedulableCatalogItem.DefaultTimeLength,
                                                  schedulableCatalogItem.Price,
                                                  schedulableCatalogItem.SchedulableCatalogTypeId,
                                                  schedulableCatalogItem.IndustryStandardCategoryId);
            _catalogContext.ServiceItems.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedulableCatalogItemById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("serviceitems/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSchedulableCatalogItemById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.ServiceItems.SingleOrDefaultAsync(ci => ci.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        //POST api/v1/[controller]/servicecategories
        [HttpPost]
        [Route("servicecategories")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddServiceCategory([FromBody]ServiceCategory schedulableCatalogType)
        {
            var item = new ServiceCategory(schedulableCatalogType.SiteId,
                                                  schedulableCatalogType.Name,
                                                  schedulableCatalogType.Description,
                                                  schedulableCatalogType.AllowOnlineScheduling,
                                                  schedulableCatalogType.ScheduleTypeId);
            _catalogContext.ServiceCategories.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedulableCatalogTypeById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("servicecategories/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceCategory), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSchedulableCatalogTypeById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.ServiceCategories.SingleOrDefaultAsync(ci => ci.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        #endregion

        #region [availabilities]

        //POST api/v1/[controller]/availabilities
        [HttpPost]
        [Route("availabilities")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddAvailability([FromBody]Availability availability)
        {
            var item = new Availability(availability.SiteId,
                                        availability.StaffId,
                                        availability.SchedulableCatalogItemId,
                                        availability.LocationId,
                                        availability.StartDateTime,
                                        availability.EndDateTime,
                                        availability.Sunday,
                                        availability.Monday,
                                        availability.Tuesday,
                                        availability.Wednesday,
                                        availability.Thursday,
                                        availability.Friday,
                                        availability.Saturday,
                                        availability.BookableEndDateTime);
            _catalogContext.Availabilities.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAvailabilityById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("availabilities/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Availability), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAvailabilityById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.Availabilities.SingleOrDefaultAsync(ci => ci.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        //POST api/v1/[controller]/unavailabilities
        [HttpPost]
        [Route("unavailabilities")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddUnavailability([FromBody]Unavailability unavailability)
        {
            var item = new Unavailability(unavailability.SiteId,
                                          unavailability.StaffId,
                                          unavailability.SchedulableCatalogItemId,
                                          unavailability.LocationId,
                                          unavailability.StartDateTime,
                                          unavailability.EndDateTime,
                                          unavailability.Sunday,
                                          unavailability.Monday,
                                          unavailability.Tuesday,
                                          unavailability.Wednesday,
                                          unavailability.Thursday,
                                          unavailability.Friday,
                                          unavailability.Saturday,
                                          unavailability.Description);
            _catalogContext.Unavailabilities.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUnavailabilityById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("unavailabilities/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Unavailability), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUnavailabilityById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.Unavailabilities.SingleOrDefaultAsync(ci => ci.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        #endregion

    }
}
