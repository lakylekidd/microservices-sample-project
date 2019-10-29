using FluentValidation;
using Microsoft.Extensions.Logging;

using Ordering.API.Application.Commands;

namespace Ordering.API.Application.CommandValidations
{
    /// <summary>
    /// The cancel order command validator
    /// </summary>
    public class CancelOrderCommandValidator 
        : AbstractValidator<CancelOrderCommand>
    {
        // The command validator
        public CancelOrderCommandValidator(ILogger<CancelOrderCommandValidator> logger)
        {
            RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

            // Log command validator instance created
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
