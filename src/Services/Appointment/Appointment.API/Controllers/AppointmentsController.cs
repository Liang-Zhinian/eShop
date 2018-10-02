using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Appointment.Domain.ReadModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaaSEqt.eShop.Services.Appointment.Domain.Commands;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    public class AppointmentsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILoggerFactory _logger;

        public AppointmentsController(IMediator mediator,
            ILoggerFactory logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody]AppointmentRM appointment, [FromHeader(Name = "x-requestid")] string requestId)
        {
            Guid commandId = Guid.NewGuid();
            appointment.Id = Guid.NewGuid();

            var makeAnAppointmentCommand = new MakeAnAppointmentCommand(
                commandId,
                appointment.Id, 
                appointment.SiteId, 
                appointment.LocationId, 
                appointment.StaffId,
                appointment.StartDateTime, appointment.EndDateTime, 
                appointment.ClientId, 
                appointment.GenderPreference,
                appointment.Duration, 
                appointment.StaffRequested, 
                appointment.Notes,
                appointment.AppointmentServiceItems,
                appointment.AppointmentResources
            );
            var result = false;

            Guid requestGuidId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
                guid : appointment.ClientId;

            var requestMakeAnAppointment = new IdentifiedCommand<MakeAnAppointmentCommand, bool>(makeAnAppointmentCommand, requestGuidId);
            result = await _mediator.Send(requestMakeAnAppointment);

            _logger.CreateLogger(nameof(AppointmentsController))
                   .LogTrace(result ? $"MakeAnAppointmentCommand has been received and a create new appointment process is started with requestId: {makeAnAppointmentCommand.ClientId}" :
                             $"MakeAnAppointmentCommand has been received but a new appointment process has failed with requestId: {makeAnAppointmentCommand.ClientId}");

            return Accepted();
        }
    }
}
