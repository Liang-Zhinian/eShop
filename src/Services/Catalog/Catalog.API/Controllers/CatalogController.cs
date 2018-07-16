using Catalog.API.IntegrationEvents;
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
using SaaSEqt.eShop.Services.Catalog.API.Model.SchedulableCatalog;

namespace SaaSEqt.eShop.Services.Catalog.API.Controllers
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

        #region [stockable catalog]

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0, [FromQuery] string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                return GetItemsByIds(ids);
            }

            var totalItems = await _catalogContext.CatalogItems
                .LongCountAsync();

            var itemsOnPage = await _catalogContext.CatalogItems
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<CatalogItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        private IActionResult GetItemsByIds(string ids)
        {
            var numIds = ids.Split(',')
                .Select(id => (Ok: int.TryParse(id, out int x), Value: x));
            if (!numIds.All(nid => nid.Ok))
            {
                return BadRequest("ids value invalid. Must be comma-separated list of numbers");
            }

            var idsToSelect = numIds.Select(id => id.Value);
            var items = _catalogContext.CatalogItems.Where(ci => idsToSelect.Contains(ci.Id)).ToList();

            items = ChangeUriPlaceholder(items);
            return Ok(items);

        }

        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _catalogContext.CatalogItems.SingleOrDefaultAsync(ci => ci.Id == id);

            var baseUri = _settings.PicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;
            item.FillProductUrl(baseUri, azureStorageEnabled: azureStorageEnabled);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        // GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items(string name, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {

            var totalItems = await _catalogContext.CatalogItems
                .Where(c => c.Name.StartsWith(name))
                .LongCountAsync();

            var itemsOnPage = await _catalogContext.CatalogItems
                .Where(c => c.Name.StartsWith(name))
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<CatalogItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        // GET api/v1/[controller]/items/type/1/brand/null[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items(int? catalogTypeId, int? catalogBrandId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<CatalogItem>)_catalogContext.CatalogItems;

            if (catalogTypeId.HasValue)
            {
                root = root.Where(ci => ci.CatalogTypeId == catalogTypeId);
            }

            if (catalogBrandId.HasValue)
            {
                root = root.Where(ci => ci.CatalogBrandId == catalogBrandId);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<CatalogItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        // GET api/v1/[controller]/CatalogTypes
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CatalogTypes()
        {
            var items = await _catalogContext.CatalogTypes
                .ToListAsync();

            return Ok(items);
        }

        // GET api/v1/[controller]/CatalogBrands
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CatalogBrands()
        {
            var items = await _catalogContext.CatalogBrands
                .ToListAsync();

            return Ok(items);
        }

        //PUT api/v1/[controller]/items
        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateProduct([FromBody]CatalogItem productToUpdate)
        {
            var catalogItem = await _catalogContext.CatalogItems
                .SingleOrDefaultAsync(i => i.Id == productToUpdate.Id);

            if (catalogItem == null)
            {
                return NotFound(new { Message = $"Item with id {productToUpdate.Id} not found." });
            }

            var oldPrice = catalogItem.Price;
            var raiseProductPriceChangedEvent = oldPrice != productToUpdate.Price;


            // Update current product
            catalogItem = productToUpdate;
            _catalogContext.CatalogItems.Update(catalogItem);

            if (raiseProductPriceChangedEvent) // Save product's data and publish integration event through the Event Bus if price has changed
            {
                //Create Integration Event to be published through the Event Bus
                var priceChangedEvent = new ProductPriceChangedIntegrationEvent(catalogItem.Id, productToUpdate.Price, oldPrice);

                // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent);

                // Publish through the Event Bus and mark the saved event as published
                await _catalogIntegrationEventService.PublishThroughEventBusAsync(priceChangedEvent);
            }
            else // Just save the updated product because the Product's Price hasn't changed.
            {
                await _catalogContext.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetItemById), new { id = productToUpdate.Id }, null);
        }

        //POST api/v1/[controller]/items
        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody]CatalogItem product)
        {
            var item = new CatalogItem
            {
                CatalogBrandId = product.CatalogBrandId,
                CatalogTypeId = product.CatalogTypeId,
                Description = product.Description,
                Name = product.Name,
                PictureFileName = product.PictureFileName,
                Price = product.Price
            };
            _catalogContext.CatalogItems.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, null);
        }

        //DELETE api/v1/[controller]/id
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _catalogContext.CatalogItems.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _catalogContext.CatalogItems.Remove(product);

            await _catalogContext.SaveChangesAsync();

            return NoContent();
        }

        private List<CatalogItem> ChangeUriPlaceholder(List<CatalogItem> items)
        {
            var baseUri = _settings.PicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            foreach (var item in items)
            {
                item.FillProductUrl(baseUri, azureStorageEnabled: azureStorageEnabled);
            }

            return items;
        }

        #endregion

        #region [schedulable catalog]

        #region [service items]

        //POST api/v1/[controller]/serviceitems
        [HttpGet]
        [Route("[action]/type/{catalogTypeId}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<SchedulableCatalogItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<SchedulableCatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindServiceItems(Guid? catalogTypeId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<SchedulableCatalogItem>)_catalogContext.SchedulableCatalogItems;

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

            var model = new PaginatedItemsViewModel<SchedulableCatalogItem>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //POST api/v1/[controller]/serviceitems
        [HttpPost]
        [Route("serviceitems")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddServiceItem([FromBody]SchedulableCatalogItem schedulableCatalogItem)
        {
            var item = new SchedulableCatalogItem(schedulableCatalogItem.SiteId,
                                                  schedulableCatalogItem.Name,
                                                  schedulableCatalogItem.Description,
                                                  schedulableCatalogItem.DefaultTimeLength,
                                                  schedulableCatalogItem.Price,
                                                  schedulableCatalogItem.SchedulableCatalogTypeId,
                                                  schedulableCatalogItem.IndustryStandardCategoryId);
            _catalogContext.SchedulableCatalogItems.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedulableCatalogItemById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("serviceitems/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SchedulableCatalogItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSchedulableCatalogItemById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.SchedulableCatalogItems.SingleOrDefaultAsync(ci => ci.Id == id);

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
        public async Task<IActionResult> AddServiceCategory([FromBody]SchedulableCatalogType schedulableCatalogType)
        {
            var item = new SchedulableCatalogType(schedulableCatalogType.SiteId,
                                                  schedulableCatalogType.Name,
                                                  schedulableCatalogType.Description,
                                                  schedulableCatalogType.AllowOnlineScheduling,
                                                  schedulableCatalogType.ScheduleTypeId);
            _catalogContext.SchedulableCatalogTypes.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedulableCatalogTypeById), new { id = item.Id }, null);
        }

        [HttpGet]
        [Route("servicecategories/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SchedulableCatalogType), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSchedulableCatalogTypeById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _catalogContext.SchedulableCatalogTypes.SingleOrDefaultAsync(ci => ci.Id == id);

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

        #endregion
    }
}
