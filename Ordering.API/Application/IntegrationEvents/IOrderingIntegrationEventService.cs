using System;
using System.Threading.Tasks;
using Microservices.Library.EventBus.Events;

namespace Ordering.API.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        /// <summary>
        /// Will publish events through the event bus async
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        Task PublishEventsThroughEventBusAsync(Guid transactionId);

        /// <summary>
        /// Will add and save the provided event async
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
