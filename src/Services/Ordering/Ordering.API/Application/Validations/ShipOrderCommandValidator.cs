﻿namespace Eva.eShop.Services.Ordering.API.Application.Validations;

public class ShipOrderCommandValidator : AbstractValidator<ShipOrderCommand>
{
    public ShipOrderCommandValidator(ILogger<ShipOrderCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
