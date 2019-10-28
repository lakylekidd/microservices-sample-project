using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Library.EventBus.Events;

namespace Microservices.Library.IntegrationEventLogEF.Services
{
    /// <summary>
    /// The integration event log service
    /// </summary>
    public interface IIntegrationEventLogService
    {
        /// <summary>
        /// Will retrieve any event logs that are pending for given transaction id
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);

        /// <summary>
        /// Will save an event async
        /// </summary>
        /// <param name="event"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);

        /// <summary>
        /// Will mark an event as published
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task MarkEventAsPublishedAsync(Guid eventId);

        /// <summary>
        /// Will mark an event as in progress
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task MarkEventAsInProgressAsync(Guid eventId);

        /// <summary>
        /// Will mark an event as failed
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
