using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Business.API.Application.Events;
using SaaSEqt.eShop.Services.Business.API.Application.Events.ServiceCategory;
using SaaSEqt.eShop.Services.Business.API.ViewModel;
using SaaSEqt.eShop.Services.Business.Domain.Model.Catalog;
using SaaSEqt.eShop.Services.Business.Infrastructure.Data;

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly BusinessDbContext _catalogContext;
        private readonly BusinessSettings _settings;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public CatalogController(BusinessDbContext context, 
                                 IOptionsSnapshot<BusinessSettings> settings,
                                 ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            _catalogContext = context ?? throw new ArgumentNullException(nameof(context));
            _catalogIntegrationEventService = catalogIntegrationEventService ?? throw new ArgumentNullException(nameof(catalogIntegrationEventService));

            _settings = settings.Value;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region [service items]

        //GET api/v1/[controller]/FindServiceItemsBy/siteId/sampleguid
        [HttpGet]
        [Route("sites/{siteId:guid}/serviceitems/{serviceItemId:guid}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceItems(Guid siteId, Guid? serviceItemId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems
                                                               .Where(y=>y.SiteId.Equals(siteId))
                                                               .Include(y=>y.ServiceCategory);

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

		//GET api/v1/[controller]/[action]/siteId/{siteId:guid}
        [HttpGet]
		[Route("sites/{siteId:guid}/servicecategories/{serviceCategoryId:guid}/serviceitems")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ServiceItems(Guid siteId, Guid? serviceCategoryId, Guid? serviceItemId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems
                                                               .Where(y => y.SiteId.Equals(siteId))
                                                               .Include(y => y.ServiceCategory);
            
            if (serviceCategoryId.HasValue) {
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

        //POST api/v1/[controller]/serviceitems
        [HttpPost]
        [Route("serviceitems")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddServiceItem([FromBody]ServiceItem schedulableCatalogItem)
        {
            var newItem = new ServiceItem(schedulableCatalogItem.SiteId,
                                        schedulableCatalogItem.Name,
                                        schedulableCatalogItem.Description,
                                        schedulableCatalogItem.DefaultTimeLength,
                                        schedulableCatalogItem.Price,
                                        schedulableCatalogItem.ServiceCategoryId,
                                        schedulableCatalogItem.IndustryStandardCategoryName,
                                        schedulableCatalogItem.IndustryStandardSubcategoryName);
            _catalogContext.ServiceItems.Add(newItem);

            ServiceItemCreatedEvent serviceItemCreatedEvent = new ServiceItemCreatedEvent(newItem.SiteId,
                                                                                          newItem.Id,
                                                                                          newItem.Name,
                                                                                          newItem.Description,
                                                                                          newItem.DefaultTimeLength,
                                                                                          newItem.Price,
                                                                                          newItem.AllowOnlineScheduling,
                                                                                          newItem.ServiceCategoryId,
                                                                                          newItem.IndustryStandardCategoryName,
                                                                                          newItem.IndustryStandardSubcategoryName);


            await _catalogIntegrationEventService.PublishThroughEventBusAsync(serviceItemCreatedEvent);


            return CreatedAtAction(nameof(ServiceItems), new { siteId =newItem.SiteId,  serviceItemId = newItem.Id }, null);
        }

        //POST api/v1/[controller]/servicecategories
        [HttpPost]
        [Route("servicecategories")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddServiceCategory([FromBody]ServiceCategory schedulableCatalogType)
        {
            var newCategory = new ServiceCategory(schedulableCatalogType.SiteId,
                                                  schedulableCatalogType.Name,
                                                  schedulableCatalogType.Description,
                                                  schedulableCatalogType.AllowOnlineScheduling,
                                                  schedulableCatalogType.ScheduleTypeId);
            _catalogContext.ServiceCategories.Add(newCategory);

            ServiceCategoryCreatedEvent serviceCategoryCreatedEvent = new ServiceCategoryCreatedEvent(newCategory.SiteId,
                                                                                                      newCategory.Id,
                                                                                                      newCategory.Name,
                                                                                                      newCategory.Description,
                                                                                                      newCategory.AllowOnlineScheduling,
                                                                                                      newCategory.ScheduleTypeId);


            await _catalogIntegrationEventService.PublishThroughEventBusAsync(serviceCategoryCreatedEvent);


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
                                                                   .Where(y=>y.SiteId.Equals(siteId))
                                                                   .Include(y=>y.ScheduleType);

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
                                        availability.ServiceItemId,
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

            return CreatedAtAction(nameof(Availabilities), new { siteId = item.SiteId, availabilityId = item.Id }, null);
        }

        //GET api/v1/[controller]/sites/{siteId:guid}/availabilities/{availabilityId:guid}
        [HttpGet]
        [Route("sites/{siteId:guid}/availabilities/{availabilityId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Availability>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Availability>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Availabilities(Guid siteId, Guid? availabilityId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<Availability>)_catalogContext.Availabilities
                                                                    .Where(y => y.SiteId.Equals(siteId));
                                            

            if (availabilityId.HasValue)
            {
                var item = await root.SingleOrDefaultAsync(ci => ci.Id == availabilityId);
                if (item != null)
                    return Ok(item);
                return BadRequest();
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .OrderBy(c => c.StartDateTime)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedItemsViewModel<Availability>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //POST api/v1/[controller]/unavailabilities
        [HttpPost]
        [Route("unavailabilities")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddUnavailability([FromBody]Unavailability unavailability)
        {
            var item = new Unavailability(unavailability.SiteId,
                                          unavailability.StaffId,
                                          unavailability.ServiceItemId,
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

            return CreatedAtAction(nameof(Unavailabilities), new { siteId=item.SiteId, unavailabilityId = item.Id }, null);
        }

        //GET api/v1/[controller]/sites/{siteId:guid}/unavailabilities/{unavailabilityId:guid}
        [HttpGet]
        [Route("sites/{siteId:guid}/unavailabilities/{unavailabilityId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Unavailability>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Unavailability>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Unavailabilities(Guid siteId, Guid? unavailabilityId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<Unavailability>)_catalogContext.Availabilities
                                                                    .Where(y => y.SiteId.Equals(siteId));


            if (unavailabilityId.HasValue)
            {
                var item = await root.SingleOrDefaultAsync(ci => ci.Id == unavailabilityId);
                if (item != null)
                    return Ok(item);
                return BadRequest();
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .OrderBy(c => c.StartDateTime)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedItemsViewModel<Unavailability>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        #endregion
    }
}
