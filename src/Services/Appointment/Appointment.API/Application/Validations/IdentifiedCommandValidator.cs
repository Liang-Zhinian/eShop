using FluentValidation;
using SaaSEqt.eShop.Services.Appointment.Domain.Commands;

namespace Appointment.API.Application.Validations
{
    public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<MakeAnAppointmentCommand,bool>>
    {
        public IdentifiedCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();    
        }
    }
}
