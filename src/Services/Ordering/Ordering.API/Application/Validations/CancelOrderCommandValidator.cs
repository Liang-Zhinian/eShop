﻿namespace Eva.eShop.Services.Ordering.API.Application.Validations;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator(ILogger<CancelOrderCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
