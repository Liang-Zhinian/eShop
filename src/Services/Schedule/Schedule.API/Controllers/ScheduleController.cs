using Schedule.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Schedule.API.Infrastructure;
using SaaSEqt.eShop.Services.Schedule.API.IntegrationEvents.Events;
using SaaSEqt.eShop.Services.Schedule.API.Model;
using SaaSEqt.eShop.Services.Schedule.API.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.Schedule.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleContext _scheduleContext;
        private readonly ScheduleSettings _settings;
        private readonly IScheduleIntegrationEventService _scheduleIntegrationEventService;

        public ScheduleController(ScheduleContext context, IOptionsSnapshot<ScheduleSettings> settings, IScheduleIntegrationEventService scheduleIntegrationEventService)
        {
            _scheduleContext = context ?? throw new ArgumentNullException(nameof(context));
            _scheduleIntegrationEventService = scheduleIntegrationEventService ?? throw new ArgumentNullException(nameof(scheduleIntegrationEventService));

            _settings = settings.Value;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


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
            _scheduleContext.Availabilities.Add(item);

            await _scheduleContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Availabilities), new { siteId = item.SiteId, availabilityId = item.Id }, null);
        }

        //GET api/v1/[controller]/sites/{siteId:guid}/locations/{locationId:guid}/availabilities
        [HttpGet]
        [Route("sites/{siteId:guid}/locations/{locationId:guid}/availabilities")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Availability>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Availabilities(Guid siteId, Guid locationId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<Availability>)_scheduleContext.Availabilities
                                                                    .Where(y => y.SiteId.Equals(siteId)
                                                                           && y.LocationId.Equals(locationId));


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

        //GET api/v1/[controller]/sites/{siteId:guid}/locations/{locationId:guid}/availabilities/{availabilityId:guid}
        [HttpGet]
        [Route("availabilities/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Availability), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Availabilities(Guid id)
        {
            var item = await _scheduleContext.Availabilities
                                             .SingleOrDefaultAsync(y => y.Id.Equals(id));


            if (item != null)
                return Ok(item);
            return BadRequest();
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
            _scheduleContext.Unavailabilities.Add(item);

            await _scheduleContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Unavailabilities), new { siteId = item.SiteId, unavailabilityId = item.Id }, null);
        }

        //GET api/v1/[controller]/sites/{siteId:guid}/locations/{locationId:guid}/unavailabilities
        [HttpGet]
        [Route("sites/{siteId:guid}/locations/{locationId:guid}/unavailabilities")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Unavailability>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Unavailabilities(Guid siteId, Guid locationId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var root = (IQueryable<Unavailability>)_scheduleContext.Unavailabilities
                                                                  .Where(y => y.SiteId.Equals(siteId)
                                                                           && y.LocationId.Equals(locationId));


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

        //GET api/v1/[controller]/unavailabilities/{id:guid}
        [HttpGet]
        [Route("unavailabilities/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Unavailability), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Unavailabilities(Guid id)
        {
            var item = await _scheduleContext.Unavailabilities
                                             .SingleOrDefaultAsync(y => y.Id.Equals(id));


            if (item != null)
                return Ok(item);
            return BadRequest();
        }

        #endregion
    }
}
