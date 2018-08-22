using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Business.API.ViewModel;
using SaaSEqt.eShop.Business.Domain.Model.Catalog;
using SaaSEqt.eShop.Business.Infrastructure.Data;

namespace SaaSEqt.eShop.Business.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly BusinessDbContext _catalogContext;
        private readonly BusinessSettings _settings;
        //private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public CatalogController(BusinessDbContext context, 
                                 IOptionsSnapshot<BusinessSettings> settings)
        {
            _catalogContext = context ?? throw new ArgumentNullException(nameof(context));
            //_catalogIntegrationEventService = catalogIntegrationEventService ?? throw new ArgumentNullException(nameof(catalogIntegrationEventService));

            _settings = settings.Value;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region [service items]

        //GET api/v1/[controller]/FindServiceItemsBy/siteId/sampleguid
        [HttpGet]
        [Route("[action]/siteId/{siteId:guid}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindServiceItemsOf(Guid? siteId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceItem>)_catalogContext.ServiceItems
                                                               .Include(y=>y.ServiceCategory);

            if (siteId.HasValue)
            {
                root = root.Where(ci => ci.SiteId == siteId);
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
                                        schedulableCatalogItem.ServiceCategoryId,
                                        schedulableCatalogItem.IndustryStandardCategoryName,
                                        schedulableCatalogItem.IndustryStandardSubcategoryName);
            _catalogContext.ServiceItems.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(FindServiceItemById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("serviceitems/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindServiceItemById(Guid id)
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

            return CreatedAtAction(nameof(FindServiceCategoryById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("servicecategories/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ServiceCategory), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindServiceCategoryById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.ServiceCategories
                                            .Include(y=>y.ScheduleType)
                                            .SingleOrDefaultAsync(ci => ci.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        //GET api/v1/[controller]/[action]/siteId/{siteId:guid}
        [HttpGet]
        [Route("[action]/siteId/{siteId:guid}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<ServiceCategory>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ServiceCategory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindServiceCategoriesOf(Guid? siteId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<ServiceCategory>)_catalogContext.ServiceCategories
                                                                   .Include(y=>y.ScheduleType);

            if (siteId.HasValue)
            {
                root = root.Where(ci => ci.SiteId == siteId);
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
