using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Library.EventBus.Abstractions;
using Microservices.Library.EventBus.Events;
using Microservices.Library.IntegrationEventLogEF;
using Microservices.Library.IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure;

namespace Ordering.API.Application.IntegrationEvents
{
    /// <summary>
    /// The ordering integration event service
    /// </summary>
    public class OrderingIntegrationEventService : IOrderingIntegrationEventService
    {
        /*
         * PRIVATE FUNCTIONS
         */

        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly OrderingContext _orderingContext;
        private readonly IntegrationEventLogContext _eventLogContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<OrderingIntegrationEventService> _logger;

        // The constructor
        public OrderingIntegrationEventService(IEventBus eventBus,
            OrderingContext orderingContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<OrderingIntegrationEventService> logger)
        {
            _orderingContext = orderingContext ?? throw new ArgumentNullException(nameof(orderingContext));
            _eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_orderingContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Publish the events through the event bus async
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            // Retrieve all the pending events to be published
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            // Loop through each integration event
            foreach (var logEvt in pendingLogEvents)
            {
                // Log publishing the event
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

                try
                {
                    // Mark the event as in progress
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    // Publish the event
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    // Mark the event as published
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    // In case of exception, log the exception
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);
                    // Mark the event as failed
                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        /// <summary>
        /// Add and save the event async
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            // Log enqueuing integration event
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);
            // Save the event async
            await _eventLogService.SaveEventAsync(evt, _orderingContext.GetCurrentTransaction());
        }
    }
}
