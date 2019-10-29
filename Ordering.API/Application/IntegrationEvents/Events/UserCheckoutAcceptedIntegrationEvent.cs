using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Library.EventBus.Events;
using Ordering.API.Application.Models;

namespace Ordering.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// An integration event fired by the basket fictional micro service.
    /// </summary>
    public class UserCheckoutAcceptedIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// The user id
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// The username
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// The city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The zip code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// The credit card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// The card holder name
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// The expiration date of the card
        /// </summary>
        public DateTime CardExpiration { get; set; }

        /// <summary>
        /// The credit card security number
        /// </summary>
        public string CardSecurityNumber { get; set; }

        /// <summary>
        /// The card type id
        /// </summary>
        public int CardTypeId { get; set; }

        /// <summary>
        /// The buyer 
        /// </summary>
        public string Buyer { get; set; }

        /// <summary>
        /// The request id
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// The customer basket
        /// </summary>
        public CustomerBasket Basket { get; }

        // The constructor
        public UserCheckoutAcceptedIntegrationEvent(string userId, string userName, string city, string street,
            string state, string country, string zipCode, string cardNumber, string cardHolderName,
            DateTime cardExpiration, string cardSecurityNumber, int cardTypeId, string buyer, Guid requestId,
            CustomerBasket basket)
        {
            UserId = userId;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipCode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            Buyer = buyer;
            Basket = basket;
            RequestId = requestId;
            UserName = userName;
        }

    }
}
