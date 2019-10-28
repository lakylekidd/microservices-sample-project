using System;
using Newtonsoft.Json;
using System.Linq;
using Microservices.Library.EventBus.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservices.Library.IntegrationEventLogEF
{
    /// <summary>
    /// Defines an integration event log entry
    /// </summary>
    public class IntegrationEventLogEntry
    {
        private IntegrationEventLogEntry() { }

        /// <summary>
        /// The event id
        /// </summary>
        public Guid EventId { get; private set; }
        /// <summary>
        /// The event type name
        /// </summary>
        public string EventTypeName { get; private set; }

        /// <summary>
        /// The event type short name
        /// </summary>
        [NotMapped]
        public string EventTypeShortName => EventTypeName.Split('.')?.Last();

        // The integration event object
        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }

        /// <summary>
        /// The state of the event
        /// </summary>
        public EventStateEnum State { get; set; }

        /// <summary>
        /// The time the event was sent
        /// </summary>
        public int TimesSent { get; set; }

        /// <summary>
        /// The creation time of the event
        /// </summary>
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// The string (serialized json) content of the event
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// The transaction id
        /// </summary>
        public string TransactionId { get; private set; }

        // The constructor
        public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
        {
            EventId = @event.Id;            
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            State = EventStateEnum.NotPublished;
            TimesSent = 0;
            TransactionId = transactionId.ToString();
        }

        /// <summary>
        /// Deserializes the json content of the event
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            IntegrationEvent = JsonConvert.DeserializeObject(Content, type) as IntegrationEvent;
            return this;
        }
    }
}
