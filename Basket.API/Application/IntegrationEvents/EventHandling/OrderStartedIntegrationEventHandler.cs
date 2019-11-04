using System;
using System.Threading.Tasks;
using Basket.API.Application.IntegrationEvents.Events;
using Basket.Domain.AggregateModels.BasketAggregate;
using Microservices.Library.EventBus.Abstractions;
using Microsoft.Extensions.Logging;

namespace Basket.API.Application.IntegrationEvents.EventHandling
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<OrderStartedIntegrationEventHandler> _logger;

        public OrderStartedIntegrationEventHandler(
            IBasketRepository repository,
            ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            //using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            //{
            //    _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            //    await _repository.DeleteBasketAsync(@event.UserId.ToString());
            //}

            await _repository.DeleteBasketAsync(@event.UserId.ToString());
        }
    }
}
