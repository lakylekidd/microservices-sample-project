using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Commands;

namespace Ordering.API.Application.CommandValidations
{
    /// <summary>
    /// A command validator that validates the <see cref="CreateOrderCommand"/>.
    /// This validator makes sure all the fields are properly populated before handing the command over to the command handler.
    /// </summary>
    public class CreateOrderCommandValidator 
        : AbstractValidator<CreateOrderCommand>
    {
        // The constructor that defines all the rules
        public CreateOrderCommandValidator(ILogger<CreateOrderCommandValidator> logger)
        {
            RuleFor(command => command.City).NotEmpty();
            RuleFor(command => command.Street).NotEmpty();
            RuleFor(command => command.State).NotEmpty();
            RuleFor(command => command.Country).NotEmpty();
            RuleFor(command => command.ZipCode).NotEmpty();
            RuleFor(command => command.CardNumber).NotEmpty().Length(12, 19);
            RuleFor(command => command.CardHolderName).NotEmpty();
            RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date");
            RuleFor(command => command.CardSecurityNumber).NotEmpty().Length(3);
            RuleFor(command => command.CardTypeId).NotEmpty();
            RuleFor(command => command.OrderItems).Must(ContainOrderItems).WithMessage("No order items found");

            // Log the creation of the command validator instance
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        // Make sure the expiration date is valid
        private static bool BeValidExpirationDate(DateTime dateTime)
        {
            return dateTime >= DateTime.UtcNow;
        }

        // Make sure order actually contains items
        private static bool ContainOrderItems(IEnumerable<CreateOrderCommand.OrderItemDTO> orderItems)
        {
            return orderItems.Any();
        }
    }
}
