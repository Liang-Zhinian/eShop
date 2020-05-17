using FluentValidation;
using Eva.eShop.Services.Ordering.API.Application.Commands;

namespace Ordering.API.Application.Validations
{
    public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateOrderCommand,bool>>
    {
        public IdentifiedCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();    
        }
    }
}
